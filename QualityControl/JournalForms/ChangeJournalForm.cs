using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server
{
    public partial class ChangeJournalForm : JournalForm
    {
        public ChangeJournalForm()
        {
            InitializeComponent();
        }

        public ChangeJournalForm(BllJournal oldJournal, BllUser user, IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            Journal = oldJournal;
            User = user;
            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            ControlMethodTabForms = new List<ControlMethodTabForm>();
            BllControl control = null;
            foreach (var controlName in controlNames)
            {
                bool isExistControl = false;

                foreach (var currentControl in oldJournal.ControlMethodsLib.Entities)
                {
                    if (currentControl.ControlName.Name == controlName.Name)
                    {
                        control = currentControl;
                        isExistControl = true;
                        break;
                    }

                }
                if (isExistControl == false)
                {
                    control = new BllControl
                    {
                        ImageLib = new BllImageLib(),
                        EquipmentLib = new BllEquipmentLib(),
                        ResultLib = new BllResultLib(),
                        ControlMethodDocumentationLib = new BllControlMethodDocumentationLib(),
                        RequirementDocumentationLib = new BllRequirementDocumentationLib(),
                        EmployeeLib = new BllEmployeeLib()
                    };
                    control.ControlName = controlName;
                    Journal.ControlMethodsLib.Entities.Add(control);
                }


                var tabForm = new ControlMethodTabForm(control, oldJournal, uow, null);
                if (control.IsControlled != null)
                {
                    tabForm.EnableFormControls();
                }
                tabForm.EnableValidateCheckBox();
                //tabForm.AddEmployee(user);
                ControlMethodTabForms.Add(tabForm);
                tabForm.FillComponents();
                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
            }



            Customers = new List<BllCustomer>();
            ICustomerService CustomerService = new CustomerService(uow);
            var customers = CustomerService.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + " " + element.Address + " " + element.Phone);

            }
            if (Customers.Count > 0)
            {
                comboBox2.SelectedItem = Journal.Customer != null ? Journal.Customer.Organization + " " + Journal.Customer.Address + " " + Journal.Customer.Phone : "";
            }

            dateTimePicker1.Value = Journal.RequestDate.Value;
            dateTimePicker2.Value = Journal.ControlDate.Value;
            numericUpDown2.Value = Journal.RequestNumber.Value;
            numericUpDown1.Value = Journal.Amount.Value < 100 ? Journal.Amount.Value : 0;
            textBox1.Text = Journal.IndustrialObject != null ? Journal.IndustrialObject.Name : "";
            textBox7.Text = Journal.WeldingType;
            textBox6.Text = Journal.WeldJoint != null ? Journal.WeldJoint.Name : "";
            textBox3.Text = Journal.Size;
            textBox2.Text = Journal.Component != null ? Journal.Component.Name : "";
            textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
            textBox5.Text = Journal.ScheduleOrganization != null ? Journal.ScheduleOrganization.Name : "";
            FillContracts(Journal.Customer);
            richTextBox2.Text = Journal.Description;
        }
    }
}
