using QualityControl_Server.Forms.ControlMethodDocumentationDirectory;
using QualityControl_Server.Forms.EmployeeDirectory;
using QualityControl_Server.Forms.EquipmentDirectory;
using QualityControl_Server.Forms.RequirementDocumentationDirectory;
using QualityControl_Server.Forms.ResultDirectory;

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
using DAL.Repositories.Interface;
using BLL.Entities;
using DAL.Repositories;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Server
{
    public partial class ControlMethodTabForm : Form
    {
        public Panel panel;
        public BllControl currentControl { get; private set; }
        private BllJournal currentJournal;

        public ControlMethodTabForm()
        {
            InitializeComponent();

        }
        string controlName;
        public ControlMethodTabForm(string controlName, IUnitOfWork uow, AddJournalForm parent)
        {
            InitializeComponent();
            addJournalForm = parent;
            this.uow = uow;
            panel = panel1;
            label1.Text = controlName;
            this.controlName = controlName;
            DisableFormControls();
            
        }
        bool isEditing = true; 
        IUnitOfWork uow;
        AddJournalForm addJournalForm;
        public ControlMethodTabForm(BllControl control, BllJournal journal, IUnitOfWork uow, AddJournalForm parent)
        {
            InitializeComponent();
            addJournalForm = parent;
            this.uow = uow;
            panel = panel1;
            SetCurrentControlAndJournal(control, journal, true);
            controlName = control.ControlName.Name;
            isEditing = true;
            //DisableFormControls();
        }


        public void SetCurrentControlAndJournal(BllControl control, BllJournal journal, bool isEditing)
        {
            this.isEditing = isEditing;
            SetCurrentControl(control);
            currentJournal = journal;
        }

        public void SetCurrentControl(BllControl control)
        {
            currentControl = control;
            label1.Text = control.ControlName.Name;
            label11.Text = control.ProtocolNumber.ToString();
            imagesForPicturebox.Clear();
            if (control.IsControlled != null)
            {
                checkBox1.Checked = true;
                SetControlCheck(control.IsControlled.Value);                
            }
            CheckedChanged();
            SetEquipment(control.EquipmentLib);
            SetImages(control.ImageLib);
            SetRequirementDocumentation(control.RequirementDocumentationLib);
            //SetControlMethodDocumentation(control.ControlMethodDocumentationLib);
            SetEmployee(control.EmployeeLib);
            SetChiefEmployee(control.ChiefEmployee);

            textBox3.Text = control.Light.ToString();
            textBox1.Text = control.Temperature.ToString();
            textBox4.Text = control.Additionally;
        }

        public void EnableValidateCheckBox()
        {
            checkBox1.Enabled = true;
        }

        public void FillComponents()
        {
            if (currentControl.IsControlled != null)
            {
                checkBox1.Checked = true;
                SetControlCheck(currentControl.IsControlled.Value);
            }
            if (currentControl.ControlMethodDocumentationLib != null)
            {
                SetControlMethodDocumentation(currentControl.ControlMethodDocumentationLib);
            }
            if (currentControl.RequirementDocumentationLib != null)
            {
                SetRequirementDocumentation(currentControl.RequirementDocumentationLib);
            }
            SetLight(currentControl.Light != null ? (float)currentControl.Light : 0);
            SetTemperature(currentControl.Temperature != null ? (float)currentControl.Temperature : 0);
            SetAdditionally(currentControl.Additionally);
            if (currentControl.EquipmentLib != null)
            {
                SetEquipment(currentControl.EquipmentLib);
            }
            if (currentControl.EmployeeLib != null)
            {
                SetEmployee(currentControl.EmployeeLib);
            }
            SetResult(currentControl.ResultLib);
            if (currentControl.ChiefEmployee != null)
            {
                SetChiefEmployee(currentControl.ChiefEmployee);
            }
        }

        private void SetChiefEmployee(BllEmployee employee)
        {
            if (employee != null)
            {
                textBox2.Text = employee.Sirname + " " + employee.Name + " " + employee.Fathername;
            }
        }

        public void SetControlCheck(bool? isControlled)
        {          
            if (isControlled == null)
            {
                checkBox1.Checked = false;
                currentControl.IsControlled = null;
                return;
            }
            if (isControlled.Value)
            {
                currentControl.IsControlled = true;
                radioButton2.Checked = true;             
            }
            else
            {
                currentControl.IsControlled = false;
                radioButton1.Checked = true;
            }
        }

        public void SetControlMethodDocumentation(BllControlMethodDocumentationLib documentationLib)
        {
            currentControl.ControlMethodDocumentationLib = documentationLib;
            if (documentationLib != null)
            {
                foreach(var documentation in documentationLib.SelectedEntities)
                {
                    listBox2.Items.Add(documentation.Entity.Name);
                }
            }
        }

        public void SetRequirementDocumentation(BllRequirementDocumentationLib documentationLib)
        {
            currentControl.RequirementDocumentationLib = documentationLib;
            listBox3.Items.Clear();
            if (documentationLib != null)
            {
                foreach (var documentation in documentationLib.SelectedEntities)
                {
                    listBox3.Items.Add(documentation.Entity.Name);
                }
            }
        }

        public void CopyNewRequirementDocumentationFromLib(BllRequirementDocumentationLib documentationLib)
        {
            currentControl.RequirementDocumentationLib.SelectedEntities.Clear();
            foreach (var doc in documentationLib.SelectedEntities)
            {
                currentControl.RequirementDocumentationLib.SelectedEntities.Add(new BllSelectedEntity<BllRequirementDocumentation> { Entity = doc.Entity });
            }
            SetRequirementDocumentation(currentControl.RequirementDocumentationLib);
        }

        public void SetLight(float light)
        {
            textBox3.Text = light.ToString();
        }

        public void SetTemperature(float temperature)
        {
            textBox1.Text = temperature.ToString();
        }

        public void SetAdditionally(string additionally)
        {
            textBox4.Text = additionally;
        }

        public void SetEquipment(BllEquipmentLib equipmentLib)
        {
            currentControl.EquipmentLib = equipmentLib;
            listBox1.Items.Clear();
            foreach (var equipment in equipmentLib.SelectedEntities)
            {
                listBox1.Items.Add(equipment.Entity.Name);              
            }

        }

        public void CopyNewEquipmentFromLib(BllEquipmentLib equipmentLib)
        {
            currentControl.EquipmentLib.SelectedEntities.Clear();
            foreach (var eq in equipmentLib.SelectedEntities)
            {
                currentControl.EquipmentLib.SelectedEntities.Add(new BllSelectedEntity<BllEquipment> { Entity = eq.Entity });
            }
            SetEquipment(currentControl.EquipmentLib);
        }

        public void SetImages(BllImageLib lib)
        {
            currentControl.ImageLib = lib;
            imagesForPicturebox.Clear();
            foreach (var image in currentControl.ImageLib.Entities)
            {
                imagesForPicturebox.Add(byteArrayToImage(image.Image));
            }
            if (imagesForPicturebox.Count > 0)
            {
                pictureBox1.Image = imagesForPicturebox[0];
            }            
            currentPositionInImages = 0;
        }

        public void CopyNewImagesFromLib(BllImageLib lib)
        {
            currentControl.ImageLib.Entities.Clear();
            foreach (var img in lib.Entities)
            {
                currentControl.ImageLib.Entities.Add(new BllImage { Image = img.Image });
            }
            SetImages(currentControl.ImageLib);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseControlMethodDocumentationForm ControlMethodDocumentationForm = new ChooseControlMethodDocumentationForm(uow);
            ControlMethodDocumentationForm.ShowDialog(this);
            BllControlMethodDocumentation controlMethodDocumentation = ControlMethodDocumentationForm.GetChosenControlMethodDocumentation();
            if (controlMethodDocumentation != null)
            {
                currentControl.ControlMethodDocumentationLib.SelectedEntities.Add(new BllSelectedEntity<BllControlMethodDocumentation> { Entity = controlMethodDocumentation });
                listBox2.Items.Add(controlMethodDocumentation.Pressmark + " " + controlMethodDocumentation.Name);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ChooseRequirementDocumentationForm RequirementDocumentationForm = new ChooseRequirementDocumentationForm(uow);
            RequirementDocumentationForm.ShowDialog(this);
            BllRequirementDocumentation requirementDocumentation = RequirementDocumentationForm.GetChosenRequirementDocumentation();
            if (requirementDocumentation != null)
            {
                currentControl.RequirementDocumentationLib.SelectedEntities.Add(new BllSelectedEntity<BllRequirementDocumentation> { Entity = requirementDocumentation });
                listBox3.Items.Add(requirementDocumentation.Pressmark + " " + requirementDocumentation.Name);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChooseEquipmentForm EquipmentForm = new ChooseEquipmentForm(uow);
            EquipmentForm.ShowDialog(this);
            List<BllEquipment> equipment = EquipmentForm.GetChosenEquipment();
            if (equipment.Count > 0)
            {
                foreach(var eq in equipment)
                {
                    currentControl.EquipmentLib.SelectedEntities.Add(new BllSelectedEntity<BllEquipment> { Entity = eq });
                    listBox1.Items.Add(eq.Name);
                }

            }
        }

        public void AddEmployee(BllEmployee employee)
        {
            if (employee != null)
            {
                currentControl.EmployeeLib.SelectedEntities.Add(new BllSelectedEntity<BllEmployee> { Entity = employee });
                listBox4.Items.Add(employee.Sirname + " " + employee.Name + " " + employee.Fathername);
            }
        }

        public void SetEmployee(BllEmployeeLib employeeLib)
        {
            currentControl.EmployeeLib = employeeLib;
            listBox4.Items.Clear();
            if (employeeLib != null)
            {
                foreach (var employee in employeeLib.SelectedEntities)
                {
                    listBox4.Items.Add(employee.Entity.Sirname + " " + employee.Entity.Name + " " + employee.Entity.Fathername);
                }
            }
        }

        public void CopyNewEmployeeFromLib(BllEmployeeLib EmployeeLib)
        {
            currentControl.EmployeeLib.SelectedEntities.Clear();
            foreach (var eq in EmployeeLib.SelectedEntities)
            {
                currentControl.EmployeeLib.SelectedEntities.Add(new BllSelectedEntity<BllEmployee> { Entity = eq.Entity });
            }
            SetEmployee(currentControl.EmployeeLib);
        }

        public void SetResult(BllResultLib ResultLib)
        {
            currentControl.ResultLib = ResultLib;
        }

        public void CopyNewResultFromLib(BllResultLib ResultLib)
        {
            currentControl.ResultLib.Entities.Clear();
            foreach (var res in ResultLib.Entities)
            {
                currentControl.ResultLib.Entities.Add(new BllResult {
                    Welder = res.Welder,
                    DefectDescription = res.DefectDescription,
                    Norm = res.Norm,
                    Number = res.Number,
                    Quality = res.Quality,
                    WeldingType = res.WeldingType
                });
            }
            SetResult(currentControl.ResultLib);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm EmployeeForm = new ChooseEmployeeForm(uow);
            EmployeeForm.ShowDialog(this);
            BllEmployee employee = EmployeeForm.GetChosenEmployee();
            if (employee != null)
            {
                currentControl.EmployeeLib.SelectedEntities.Add(new BllSelectedEntity<BllEmployee> { Entity = employee });
                listBox4.Items.Add(employee.Sirname + " " + employee.Name + " " + employee.Fathername);
            }
            if (addJournalForm != null)
            {
                addJournalForm.AddCurrentEmployeeLibToAllMethods();
            }
        }

        List<Image> imagesForPicturebox = new List<Image>();
        int currentPositionInImages = 0;

        private void button11_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                var image = new BllImage
                {
                    Image = imageToByteArray(Image.FromFile(openFileDialog1.FileName))
                };
                currentControl.ImageLib.Entities.Add(image);
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                imagesForPicturebox.Add(Image.FromFile(openFileDialog1.FileName));
                currentPositionInImages = imagesForPicturebox.Count - 1;
            }

        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }

        }

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (currentPositionInImages > 0)
            {
                currentPositionInImages--;
                pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (currentPositionInImages < imagesForPicturebox.Count - 1)
            {
                currentPositionInImages++;
                pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (imagesForPicturebox.Count > 0)
            {
                ImageService imageService = new ImageService(uow);
                if (currentControl.ImageLib.Entities[currentPositionInImages].Id != 0)
                {
                    imageService.Delete(currentControl.ImageLib.Entities[currentPositionInImages].Id);
                }
                imagesForPicturebox.RemoveAt(currentPositionInImages);
                currentControl.ImageLib.Entities.RemoveAt(currentPositionInImages);
                if (currentPositionInImages > 0)
                {
                    currentPositionInImages--;
                }
                if (imagesForPicturebox.Count > 0)
                {
                    pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (currentControl != null)
            {
                if (radioButton.Checked)
                {
                    currentControl.IsControlled = false;
                }
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (currentControl != null)
            {
                if (radioButton.Checked)
                {
                    currentControl.IsControlled = true;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            float light;
            if (textBox3.Text != "")
            {
                if (float.TryParse(textBox3.Text, out light))
                {
                    if (currentControl != null)
                    {
                        currentControl.Light = light;
                    }

                }
                else
                {
                    MessageBox.Show("Неверный формат числа (Освещённость)", "Ошибка ввода");
                    textBox3.Text = "";
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                currentControl.Additionally = textBox4.Text;
            }
            
        }

        public void ClearData()
        {
            checkBox1.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            imagesForPicturebox.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            listBox1.Items.Clear();
            pictureBox1.Image = null;
            listBox4.Items.Clear();
            ClearProtocolNumber();
            currentControl = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (currentControl != null)
            {
                ResultDirectoryForm resultDirectoryForm = new ResultDirectoryForm(currentControl.ResultLib, isEditing);
                resultDirectoryForm.ShowDialog(this);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                currentControl.ControlMethodDocumentationLib.SelectedEntities.RemoveAt(listBox2.SelectedIndex);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                currentControl.RequirementDocumentationLib.SelectedEntities.RemoveAt(listBox3.SelectedIndex);
                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                currentControl.EquipmentLib.SelectedEntities.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                currentControl.EmployeeLib.SelectedEntities.RemoveAt(listBox4.SelectedIndex);
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        public void DisableFormControls()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button9.Visible = false;
            button11.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox1.ReadOnly = true;
            //ClearProtocolNumber();
            
        }

        public void EnableFormControls()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button9.Visible = true;
            button11.Visible = true;
            button12.Enabled = true;
            button13.Visible = true;
            button14.Visible = true;
            button15.Visible = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            textBox1.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            //ClearProtocolNumber();
        }

        public void ClearProtocolNumber()
        {
            label11.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void CheckedChanged()
        {
            if (checkBox1.Checked)
            {
                EnableFormControls();
                if (currentControl.IsControlled == null)
                {
                    SetControlCheck(true);
                }
                else
                {
                    SetControlCheck(currentControl.IsControlled.Value);
                }
            }
            else
            {
                DisableFormControls();
                currentControl.IsControlled = null;
                radioButton1.Checked = false;
                radioButton1.Checked = false;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Dock == DockStyle.None)
            {
                pb.Dock = DockStyle.Fill;
                pb.BringToFront();
            }
            else
            {
                pb.Dock = DockStyle.None;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            float temperature;
            if (textBox1.Text != "")
            {
                if (float.TryParse(textBox1.Text, out temperature))
                {
                    if (currentControl != null)
                    {
                        currentControl.Temperature = temperature;
                    }

                }
                else
                {
                    MessageBox.Show("Неверный формат числа (Температура)", "Ошибка ввода");
                    textBox1.Text = "";
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm EmployeeForm = new ChooseEmployeeForm(uow);
            EmployeeForm.ShowDialog(this);
            BllEmployee employee = EmployeeForm.GetChosenEmployee();
            if (employee != null)
            {
                currentControl.ChiefEmployee = employee;
                textBox2.Text = employee.Sirname + " " + employee.Name + " " + employee.Fathername;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            currentControl.ChiefEmployee = null;
        }

        public bool? IsControlled()
        {
            if (currentControl != null)
            {
                return currentControl.IsControlled;
               
            }
            return null;
        }
    }
}
