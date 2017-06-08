using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.RequirementDocumentationDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.TemplateDirectory
{
    public partial class ChangeTemplateForm : ChangeForm
    {
        BllTemplate oldTemplate;
        BllMaterial material;
        BllWeldJoint weldJoint = null;
        BllCustomer customer;
        BllIndustrialObject industrialObject;
        BllControlMethodsLib controlMethodsLib;
        List<ControlMethodTabForm> ControlMethodTabForms;
        List<BllIndustrialObject> IndustrialObjects;
        List<BllControlName> ControlNames;
        List<BllCustomer> Customers;
        IUnitOfWork uow;

        public ChangeTemplateForm() : base()
        {
            InitializeComponent();
        }
        public ChangeTemplateForm(DirectoryForm parent, BllTemplate oldTemplate, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            this.oldTemplate = oldTemplate;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();

            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            ControlNames = new List<BllControlName>();
            ControlMethodTabForms = new List<ControlMethodTabForm>();
            controlMethodsLib = new BllControlMethodsLib();

            textBox1.Text = oldTemplate.Name;
            textBox3.Text = oldTemplate.Size;
            textBox4.Text = oldTemplate.Material != null ? oldTemplate.Material.Name : "";
            textBox5.Text = oldTemplate.WeldJoint != null ? oldTemplate.WeldJoint.Name : "";
            textBox6.Text = oldTemplate.WeldingType;

            IndustrialObjects = new List<BllIndustrialObject>();
            IIndustrialObjectService industrialObjectService = new IndustrialObjectService(uow);
            var industrialObjects = industrialObjectService.GetAll();
            foreach (var element in industrialObjects)
            {
                IndustrialObjects.Add(element);
                comboBox1.Items.Add(element.Name);
            }
            if (IndustrialObjects.Count > 0)
            {
                comboBox1.SelectedValue = oldTemplate.IndustrialObject != null ? oldTemplate.IndustrialObject.Name : "";
            }

            Customers = new List<BllCustomer>();
            ICustomerService CustomerService = new CustomerService(uow);
            var customers = CustomerService.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + "  " + element.Address + "  " + element.Phone);

            }
            if (Customers.Count > 0)
            {
                comboBox1.SelectedValue = oldTemplate.Customer != null ? oldTemplate.Customer.Organization + " " + oldTemplate.Customer.Address + " " + oldTemplate.Customer.Phone : "";
            }

            textBox9.Text = oldTemplate.Contract;
            richTextBox2.Text = oldTemplate.Description;

            material = oldTemplate.Material;
            weldJoint = oldTemplate.WeldJoint;
            customer = oldTemplate.Customer;
            industrialObject = oldTemplate.IndustrialObject;
            controlMethodsLib = oldTemplate.ControlMethodsLib;

            foreach (var controlName in controlNames)
            {
                BllControl control = null;
                bool isExistControl = false;

                foreach (var currentControl in oldTemplate.ControlMethodsLib.Control)
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
                    oldTemplate.ControlMethodsLib.Control.Add(control);
                }


                var tabForm = new ControlMethodTabForm(control, null, uow, null);
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


        }

        private void RemoveUncontrolledMethods()
        {
            var controls = controlMethodsLib.Control;
            int j = 0;
            List<BllControl> controlsForRemoving = new List<BllControl>();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].IsControlled == null)
                {
                    controlsForRemoving.Add(controls[i]);
                }

            }

            foreach (var control in controlsForRemoving)
            {
                controls.Remove(control);
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                RemoveUncontrolledMethods();
                BllTemplate template = new BllTemplate
                {
                    Contract = textBox9.Text,
                    ControlMethodsLib = controlMethodsLib,
                    Customer = comboBox2.SelectedIndex != -1 ? Customers[comboBox1.SelectedIndex] : null,
                    Description = richTextBox2.Text,
                    IndustrialObject = comboBox1.SelectedIndex != -1 ? IndustrialObjects[comboBox1.SelectedIndex] : null,
                    Material = material,
                    Size = textBox3.Text,
                    Name = textBox1.Text,
                    WeldingType = textBox6.Text,
                    WeldJoint = weldJoint
                };
                ITemplateService Service = new TemplateService(uow);
                Service.Update(template);
                base.button2_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm chooseMaterialForm = new ChooseMaterialForm(uow);
            chooseMaterialForm.ShowDialog(this);
            material = chooseMaterialForm.GetChosenMaterial();
            if (material != null)
            {
                textBox4.Text = material.Name;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChooseWeldJointForm chooseWeldJointForm = new ChooseWeldJointForm(uow);
            chooseWeldJointForm.ShowDialog(this);
            weldJoint = chooseWeldJointForm.GetChosenWeldJoint();
            if (weldJoint != null)
            {
                textBox5.Text = weldJoint.Name;
            }
        }


      
    }
}
