using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms.EquipmentDirectory;
using QualityControl_Server.Forms.MaterialDirectory;
using QualityControl_Server.Forms.RequirementDocumentationDirectory;
using QualityControl_Server.Forms.WeldJointDirectory;
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


namespace QualityControl_Server.Forms.TemplateDirectory
{
    public partial class AddTemplateForm : AddForm
    {
      
        BllMaterial material;
        BllWeldJoint weldJoint = null;
        BllControlMethodsLib controlMethodsLib;
        List<ControlMethodTabForm> ControlMethodTabForms;
        List<BllIndustrialObject> IndustrialObjects;
        List<BllControlName> ControlNames;
        List<BllCustomer> Customers;


        public AddTemplateForm() : base()
        {
            InitializeComponent();
        }
        public AddTemplateForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();

            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            ControlNames = new List<BllControlName>();
            ControlMethodTabForms = new List<ControlMethodTabForm>();
            controlMethodsLib = new BllControlMethodsLib();
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
                controlMethodsLib.Entities.Add(control);
                var tabForm = new ControlMethodTabForm(controlName.Name, uow, null);
                tabForm.SetCurrentControl(control);
                tabForm.EnableValidateCheckBox();
                ControlMethodTabForms.Add(tabForm);

                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
                ControlNames.Add(controlName);
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

        protected void button1_Click(object sender, EventArgs e)
        {
            Close();
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

        private void RemoveUncontrolledMethods()
        {
            var controls = controlMethodsLib.Entities;
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

                ITemplateService service = new TemplateService(uow);
                service.Create(template);
                base.button2_Click(sender, e);
            }
        }
    }
}
