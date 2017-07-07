using QualityControl_Server.Forms.ComponentDirectory;
using QualityControl_Server.Forms.MaterialDirectory;
using QualityControl_Server.Forms.WeldJointDirectory;
using QualityControl_Server;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Entities;
using BLL.Services.Interface;
using BLL.Services;
using DAL.Repositories.Interface;

namespace QualityControl_Server
{
    public partial class AddJournalForm : JournalForm
    {
        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        List<BllControlName> ControlNames;

        public delegate void AddRowToDataGrid(BllJournal journal);

        private AddRowToDataGrid AddRowToDataGridDelegate;

        public AddJournalForm()
        {
            InitializeComponent();
        }

        public AddJournalForm(AddRowToDataGrid AddRowToDataGridDelegate, BllUser user, IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;


            User = user;
            dateTimePicker1.Focus();
            this.AddRowToDataGridDelegate = AddRowToDataGridDelegate;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            Journal = new BllJournal
            {
                RequestDate = DateTime.Now,
                ControlDate = DateTime.Now,
                RequestNumber = 0,
                Amount = 0,

            };
            Journal.ControlMethodsLib = new BllControlMethodsLib();

            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            ControlNames = new List<BllControlName>();
            ControlMethodTabForms = new List<ControlMethodTabForm>();

            foreach (var controlName in controlNames)
            {
                var control = new BllControl
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

                var tabForm = new ControlMethodTabForm(controlName.Name, uow, this);
                tabForm.EnableValidateCheckBox();
                ControlMethodTabForms.Add(tabForm);

                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
                ControlNames.Add(controlName);
            }

            for (int i = 0; i < ControlNames.Count; i++)
            {
                var control = Journal.ControlMethodsLib.Entities[i];
                ControlMethodTabForms[i].SetCurrentControlAndJournal(control, Journal);
                ControlMethodTabForms[i].AddEmployee(user.Employee);
            }

            IndustrialObjects = new List<BllIndustrialObject>();
            IIndustrialObjectService industrialObjectService = new IndustrialObjectService(uow);
            var industrialObjects = industrialObjectService.GetAll();
            foreach (var element in industrialObjects)
            {
                IndustrialObjects.Add(element);
                comboBox1.Items.Add(element.Name);
            }

            Customers = new List<BllCustomer>();
            ICustomerService CustomerService = new CustomerService(uow);
            var customers = CustomerService.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + " " + element.Address + " " + element.Phone);

            }

        }

        protected override void button4_Click(object sender, EventArgs e)
        {
            InitializeJournalViaFormControls();
            if (DealWithUnfilledInfo())
            {
                IJournalService Service = new JournalService(uow);

                List<BllControl> temp = Clone(Journal.ControlMethodsLib.Entities); //оставляю для следующих добавляемых объектов в этом окне
                RemoveUncontrolledMethods();


                Journal = Service.Create(Journal);
                MessageBox.Show("Информация добавлена.", "Оповещение");
                AddRowToDataGridDelegate(Journal);

                for (int i = 0; i < temp.Count; i++)
                {
                    foreach (var createdControl in Journal.ControlMethodsLib.Entities)
                    {
                        if (temp[i].ControlName.Id == createdControl.ControlName.Id)
                        {
                            temp[i].Id = createdControl.Id;
                            temp[i].ProtocolNumber = createdControl.ProtocolNumber;
                        }
                    }
                }
                Journal = CloneJournal(Journal);
                Journal.ControlMethodsLib.Entities = temp;

                AppConfigManager configManager = new AppConfigManager();
                if (bool.Parse(configManager.GetTagValue(configManager.clearEquipmentAfterAdding)))
                {
                    ClearEquipment();
                }
                if (bool.Parse(configManager.GetTagValue(configManager.clearDefectsAfterAdding)))
                {
                    ClearControlMethodResult();
                }
                if (bool.Parse(configManager.GetTagValue(configManager.clearEmployeesAfterAdding)))
                {
                    ClearEmployees();
                }


                for (int i = 0; i < ControlNames.Count; i++)
                {
                    var control = Journal.ControlMethodsLib.Entities[i];
                    ControlMethodTabForms[i].SetCurrentControlAndJournal(control, Journal);
                }
                isClosed = false;
            }
        }

        protected override void button5_Click(object sender, EventArgs e)
        {
            isClosed = true;
            Close();
        }


        private List<BllControl> Clone(List<BllControl> source)
        {
            List<BllControl> temp = new List<BllControl>();
            foreach (var elem in source)
            {
                temp.Add(CloneControl(elem));
            }
            return temp;
        }

        private BllControl CloneControl(BllControl source)
        {
            BllControl target = new BllControl();
            target.Additionally = source.Additionally;
            target.ControlName = source.ControlName;
            target.Id = source.Id;
            target.IsControlled = source.IsControlled;
            target.Light = source.Light;
            target.Number = source.Number;
            target.ProtocolNumber = source.ProtocolNumber;
            target.ControlMethodDocumentationLib = new BllControlMethodDocumentationLib();
            target.Temperature = source.Temperature;
            foreach (var doc in source.ControlMethodDocumentationLib.SelectedEntities)
            {
                target.ControlMethodDocumentationLib.SelectedEntities.Add(doc);
            }

            target.EmployeeLib = new BllEmployeeLib();
            foreach (var employee in source.EmployeeLib.SelectedEntities)
            {
                target.EmployeeLib.SelectedEntities.Add(employee);
            }

            target.EquipmentLib = new BllEquipmentLib();
            foreach (var Equipment in source.EquipmentLib.SelectedEntities)
            {
                target.EquipmentLib.SelectedEntities.Add(Equipment);
            }

            target.RequirementDocumentationLib = new BllRequirementDocumentationLib();
            foreach (var RequirementDocumentation in source.RequirementDocumentationLib.SelectedEntities)
            {
                target.RequirementDocumentationLib.SelectedEntities.Add(RequirementDocumentation);
            }

            target.ResultLib = new BllResultLib();
            foreach (var Result in source.ResultLib.Entities)
            {
                target.ResultLib.Entities.Add(Result);
            }

            target.ImageLib = new BllImageLib();
            foreach (var Image in source.ImageLib.Entities)
            {
                target.ImageLib.Entities.Add(Image);
            }

            target.ChiefEmployee = source.ChiefEmployee;

            return target;

        }

        private BllJournal CloneJournal(BllJournal source)
        {
            BllJournal target = new BllJournal();
            target.Amount = source.Amount;
            target.Component = source.Component;
            target.Contract = source.Contract;
            target.ControlDate = source.ControlDate;
            target.Customer = source.Customer;
            target.Description = source.Description;
            target.Id = source.Id;
            target.IndustrialObject = source.IndustrialObject;
            target.Material = source.Material;
            target.RequestDate = source.RequestDate;
            target.RequestNumber = source.RequestNumber;
            target.Size = source.Size;
            target.WeldJoint = source.WeldJoint;
            target.ControlMethodsLib = new BllControlMethodsLib();
            return target;
        }


        private bool DealWithUnfilledInfo()
        {
            string unfilledInfo = "";
            if (Journal.Component == null)
            {
                unfilledInfo += "\n Объект контроля";
            }
            if (Journal.Material == null)
            {
                unfilledInfo += "\n Материал";
            }
            if (Journal.Size == "")
            {
                unfilledInfo += "\n Типовой размер";
            }
            if (Journal.WeldJoint == null)
            {
                unfilledInfo += "\n Тип сварного соединения";
            }
            if (Journal.IndustrialObject == null)
            {
                unfilledInfo += "\n Промышленный объект";
            }
            if (Journal.Customer == null)
            {
                unfilledInfo += "\n Заказчик";
            }
            if (Journal.Contract == null)
            {
                unfilledInfo += "\n Основание/договор";
            }

            if (unfilledInfo != "")
            {
                ContinueChoiceForm form = new ContinueChoiceForm("Не заполнены поля: " + unfilledInfo + "\nЖелаете продолжить?");
                form.ShowDialog();
                return form.IsContinue;
            }
            return true;

        }


        private void ClearEquipment()
        {
            foreach (var control in Journal.ControlMethodsLib.Entities)
            {
                control.EquipmentLib = new BllEquipmentLib();
            }
        }

        private void ClearControlMethodResult()
        {
            foreach (var control in Journal.ControlMethodsLib.Entities)
            {
                control.ResultLib = new BllResultLib();
            }
        }

        private void ClearEmployees()
        {
            foreach (var control in Journal.ControlMethodsLib.Entities)
            {
                control.EmployeeLib = new BllEmployeeLib();
            }
        }

        public void AddCurrentEmployeeLibToAllMethods()
        {
            AppConfigManager configManager = new AppConfigManager();
            if (bool.Parse(configManager.GetTagValue(configManager.copyEmployeesToAllTypesOfMethods)))
            {
                var currentControl = ControlMethodTabForms[tabControl1.SelectedIndex].currentControl;
                for (int i = 0; i < Journal.ControlMethodsLib.Entities.Count; i++)
                {
                    if (!currentControl.Equals(Journal.ControlMethodsLib.Entities[i]))
                    {
                        ControlMethodTabForms[i].CopyNewEmployeeFromLib(currentControl.EmployeeLib);
                    }
                }
            }
        }


    }
}
