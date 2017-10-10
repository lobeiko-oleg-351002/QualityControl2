using QualityControl_Server.Forms.ComponentDirectory;
using QualityControl_Server.Forms.MaterialDirectory;
using QualityControl_Server.Forms.WeldJointDirectory;
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
using DAL.Repositories.Interface;
using BLL.Services;
using QualityControl_Server.DirectoryForms.ScheduleOrganizationDirectory;
using QualityControl_Server.Forms.IndustrialObjectDirectory;

namespace QualityControl_Server
{
    public partial class JournalForm : Form
    {
        protected List<ControlMethodTabForm> ControlMethodTabForms;
        protected List<BllCustomer> Customers;
        protected List<BllContract> Contracts;
        protected BllUser User;
        protected IComponentService componentService;
        protected List<LiteComponent> Components = new List<LiteComponent>();
        //protected BllIndustrialObject IndustrialObject;

        public BllJournal Journal = new BllJournal();

        public JournalForm()
        {
            InitializeComponent();
        }
        protected IUnitOfWork uow;
        public JournalForm(BllJournal oldJournal, BllUser user, IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;

 
            InitializeComponentComboBox();
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
            Contracts = new List<BllContract>();
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

            dateTimePicker2.Value = Journal.ControlDate.Value;
            numericUpDown2.Value = Journal.RequestNumber.Value;
            numericUpDown1.Value = Journal.Amount.Value < 100 ? Journal.Amount.Value : 0;
            textBox1.Text = Journal.IndustrialObject != null ? Journal.IndustrialObject.Name : "";
            textBox3.Text = Journal.Weight;
            comboBox3.Text = Journal.Component != null ? Journal.Component.Name + " " + Journal.Component.Pressmark : "";

            textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
            textBox5.Text = Journal.ScheduleOrganization != null ? Journal.ScheduleOrganization.Name : "";
            FillContracts(Journal.Customer);
            richTextBox2.Text = Journal.Description;
        }

        protected void InitializeComponentComboBox()
        {
            comboBox3.Items.Clear();
            componentService = new ComponentService(uow);
            Components = componentService.GetAllLite();
            comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
          //  comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            foreach(var item in Components)
            {
                comboBox3.Items.Add(item.NameAndPressmark);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm(uow);
            ComponentForm.ShowDialog(this);
            InitializeComponentComboBox();
            BllComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                SetComponent(Component);

            }
        }

        protected virtual void SetComponent(BllComponent entity)
        {
            Journal.Component = entity;
            comboBox3.Text = entity.Name + " " + entity.Pressmark;
            if (entity.IndustrialObject != null)
            {
                SetIndustrialObject(entity.IndustrialObject);
            }
            if (entity.Count != null)
            {

                label1.Visible = true;
                label9.Visible = true;
                int controlledCount = uow.Journals.GetControlledCount(entity.Id);
                int count = entity.Count.Value - controlledCount;
                if (count > 0)
                {
                    label9.Text = count.ToString();
                    numericUpDown1.Value = count;
                }
                else
                {
                    label9.Text = entity.Count.Value.ToString() + " - " + controlledCount.ToString() + " (пересорт)";
                    numericUpDown1.Value = 1;
                }
            }
            if (entity.Description != null)
            {
                textBox3.Text = entity.Description;
            }
        }

        protected void SetIndustrialObject(BllIndustrialObject entity)
        {
            Journal.IndustrialObject = entity;
            textBox1.Text = entity.Name;
        }

        private void SetMaterial(BllMaterial entity)
        {
            Journal.Material = entity;
            textBox4.Text = entity.Name;
        }

        private void SetScheduleOrganization(BllScheduleOrganization entity)
        {
            Journal.ScheduleOrganization = entity;
            textBox5.Text = entity.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm MaterialForm = new ChooseMaterialForm(uow);
            MaterialForm.ShowDialog(this);
            BllMaterial Material = MaterialForm.GetChosenMaterial();
            if (Material != null)
            {
                SetMaterial(Material);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseScheduleOrganizationForm ScheduleOrganizationForm = new ChooseScheduleOrganizationForm(uow);
            ScheduleOrganizationForm.ShowDialog(this);
            BllScheduleOrganization ScheduleOrganization = ScheduleOrganizationForm.GetChosenScheduleOrganization();
            if (ScheduleOrganization != null)
            {
                SetScheduleOrganization(ScheduleOrganization);
            }
        }

        protected void RemoveUncontrolledMethods()
        {
            var controls = Journal.ControlMethodsLib.Entities;
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

        private List<BllControl> Clone(List<BllControl> source)
        {
            List<BllControl> temp = new List<BllControl>();
            foreach (var elem in source)
            {
                temp.Add(elem);
            }
            return temp;
        }

        protected virtual void button4_Click(object sender, EventArgs e)
        {
            IJournalService Service = new JournalService(uow);
            InitializeJournalViaFormControls();
            //List<BllControl> temp = Clone(Journal.ControlMethodsLib.Control);
            RemoveUncontrolledMethods();
            Journal.ModifiedDate = DateTime.Now;
            Journal.UserModifierLogin = User != null ? User.Login : "";

            Journal = Service.Update(Journal);

            isClosed = false;
            MessageBox.Show("Информация обновлена", "Оповещение");
            Close();
        }

        public bool isClosed { get; protected set; }

        protected virtual void button5_Click(object sender, EventArgs e)
        {
            isClosed = true;
            IJournalService Service = new JournalService(uow);
            Journal = Service.Get(Journal.Id);
            Close();
        }

        protected void InitializeJournalViaFormControls()
        {
            Journal.ControlDate = dateTimePicker2.Value;
            Journal.RequestNumber = (int)numericUpDown2.Value;
            Journal.Amount = (int)numericUpDown1.Value;
            Journal.Weight = textBox3.Text;
            Journal.Description = richTextBox2.Text;
            Journal.UserOwner = User;

        }

        protected void InitializeFormControlsViaJournal(BllJournal Journal)
        {
            if (Customers.Count > 0)
            {
                comboBox2.SelectedItem = Journal.Customer != null ? Journal.Customer.Organization + " " + Journal.Customer.Address + " " + Journal.Customer.Phone : "";
                FillContracts(Journal.Customer);
            }

            dateTimePicker2.Value = Journal.ControlDate.Value;
            numericUpDown2.Value = Journal.RequestNumber.Value;
            numericUpDown1.Value = Journal.Amount.Value;
            textBox3.Text = Journal.Weight;
            comboBox3.Text = Journal.Component != null ? Journal.Component.Name + " " + Journal.Component.Pressmark : "";
            textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
            textBox5.Text = Journal.ScheduleOrganization != null ? Journal.ScheduleOrganization.Name : "";
            textBox1.Text = Journal.IndustrialObject != null ? Journal.IndustrialObject.Name : "";
            richTextBox2.Text = Journal.Description;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Journal.Component != null)
            {
                var template = Journal.Component.Template;
                if (template != null)
                {
                    Journal.Material = template.Material;
                    Journal.ScheduleOrganization = template.ScheduleOrganization;
                    Journal.Contract = template.Contract;
                    //Journal.ControlMethodsLib = template.ControlMethodsLib;
                    UncheckAllMethods();
                    foreach(var control in template.ControlMethodsLib.Entities)
                    {
                        for(int i = 0; i < Journal.ControlMethodsLib.Entities.Count; i++)
                        {
                            if (control.ControlName.Id == Journal.ControlMethodsLib.Entities[i].ControlName.Id)
                            {
                                Journal.ControlMethodsLib.Entities[i] = control;
                            }
                        }
                    }
                    Journal.Customer = template.Customer;
                    Journal.Description = template.Description;
                    Journal.IndustrialObject = template.IndustrialObject;
                    Journal.Weight = template.Weight;

                    if (Journal.Component != null)
                    {
                        if (Journal.Component.IndustrialObject != null)
                        {
                            Journal.IndustrialObject = Journal.Component.IndustrialObject;
                        }
                    }


                    InitializeFormControlsViaJournal(Journal);

                    foreach (var control in template.ControlMethodsLib.Entities)
                    {
                        for (int i = 0; i < ControlMethodTabForms.Count; i++)
                        {
                            if (ControlMethodTabForms[i].currentControl.ControlName.Name.Equals(control.ControlName.Name))
                            {
                                ControlMethodTabForms[i].SetCurrentControl(control);

                            }
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Шаблон не указан", "Оповещение");
                }
            }
            else
            {
                MessageBox.Show("Выберите объект контроля", "Оповещение");
            }
        }

        private void UncheckAllMethods()
        {
            foreach (var control in ControlMethodTabForms)
            {
                control.SetControlCheck(null);
            }
        }

        protected void FillContracts(BllCustomer customer)
        {
            const string format = "dd.MM.yyyy";
            comboBox1.Items.Clear();
            Contracts.Clear();
            if (comboBox2.SelectedIndex != -1)
            {
                foreach (var contract in Customers[comboBox2.SelectedIndex].ContractLib.Entities)
                {
                    Contracts.Add(contract);
                    comboBox1.Items.Add(contract.Name + " " + contract.BeginDate.Value.ToString(format) + " - " + contract.EndDate.Value.ToString(format));
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Journal.Customer = comboBox2.SelectedIndex != -1 ? Customers[comboBox2.SelectedIndex] : null;
            FillContracts(Customers[comboBox2.SelectedIndex]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var currentControl = ControlMethodTabForms[tabControl1.SelectedIndex].currentControl;
            for (int i = 0; i < Journal.ControlMethodsLib.Entities.Count; i++)
            {
                if (!currentControl.Equals(Journal.ControlMethodsLib.Entities[i]))
                {
                    var controls = Journal.ControlMethodsLib.Entities;
                    ControlMethodTabForms[i].CopyNewEmployeeFromLib(currentControl.EmployeeLib);
                    ControlMethodTabForms[i].SetLight(currentControl.Light != null ? (float)currentControl.Light : 0);
                    ControlMethodTabForms[i].CopyNewResultFromLib(currentControl.ResultLib);
                    ControlMethodTabForms[i].SetAdditionally(currentControl.Additionally);
                    //ControlMethodTabForms[i].CopyNewEquipmentFromLib(currentControl.EquipmentLib);
                    ControlMethodTabForms[i].CopyNewImagesFromLib(currentControl.ImageLib);
                    ControlMethodTabForms[i].CopyNewRequirementDocumentationFromLib(currentControl.RequirementDocumentationLib);
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChooseIndustrialObjectForm IndustrialObjectForm = new ChooseIndustrialObjectForm(uow);
            IndustrialObjectForm.ShowDialog(this);
            BllIndustrialObject industrialObject = IndustrialObjectForm.GetChosenIndustrialObject();
            if (industrialObject != null)
            {
                SetIndustrialObject(industrialObject);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Journal.Contract = comboBox1.SelectedIndex != -1 ? Contracts[comboBox1.SelectedIndex] : null;
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {


        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach(var item in Components)
            {
                if(item.NameAndPressmark.IndexOf(comboBox3.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    comboBox3.Items.Add(item.NameAndPressmark);
                }
            }
            if (comboBox3.Items.Count > 1 && comboBox3.Text != "")
            {
                comboBox3.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }
            if (comboBox3.Text.Length > 0)
            {
                comboBox3.SelectionStart = comboBox3.Text.Length; // add some logic if length is 0
                comboBox3.SelectionLength = 0;
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Journal.Amount = (int?)numericUpDown1.Value;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                foreach (var item in Components)
                {
                    if (item.NameAndPressmark == (string)comboBox3.SelectedItem)
                    {
                        SetComponent(componentService.Get(item.Id));
                    }
                }
            }
            else
            {
                comboBox3.Text = "";
                Journal.Component = null;
            }
        }
    }
}