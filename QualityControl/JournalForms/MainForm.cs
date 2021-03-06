﻿using Microsoft.Office.Interop.Word;
using ORM;
using QualityControl_Server;
using QualityControl_Server.Forms;
using QualityControl_Server.Forms.ComponentDirectory;
using QualityControl_Server.Forms.ControlMethodDocumentationDirectory;
using QualityControl_Server.Forms.CustomerDirectory;
using QualityControl_Server.Forms.EmployeeDirectory;
using QualityControl_Server.Forms.EquipmentDirectory;
using QualityControl_Server.Forms.IndustrialObjectDirectory;
using QualityControl_Server.Forms.MaterialDirectory;
using QualityControl_Server.Forms.RequirementDocumentationDirectory;
using QualityControl_Server.Forms.TemplateDirectory;
using QualityControl_Server.Forms.UserDirectory;
using QualityControl_Server.Forms.WeldJointDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Entities;
using BLL.Services.Interface;
using BLL.Services;
using DAL.Repositories.Interface;
using DAL.Repositories;
using System.Configuration;
using QualityControl_Server.DirectoryForms.RawDirectory;
using QualityControl_Server.DirectoryForms.ScheduleOrganizationDirectory;

namespace QualityControl
{
    public partial class MainForm : Form
    {
        List<LiteJournal> Journals;
        List<BllControlName> ControlNames;
        List<ControlMethodTabForm> ControlMethodTabForms;
        IJournalService journalService;
        BllUser User = null;
        BllJournal selectedJournal = null;
        Func<BllJournal, bool> filtration = (BllJournal journal) => { return true; };

        bool isActivatedLicense = false;
        bool isConnectedToServer = false;
        bool isFirstStart = true;

        ServiceDB serviceDB;
        IUnitOfWork uow;
        public MainForm()
        {
            InitializeComponent();

        }

        

        private void PutScrollBarDown()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
            }
        }

        private void SetGuestPermission()
        {
            администрированиеToolStripMenuItem.Enabled = false;
            добавитьToolStripMenuItem.Enabled = false;
            редактироватьToolStripMenuItem.Enabled = false;
            удалитьToolStripMenuItem.Enabled = false;
            toolStripMenuItem3.Enabled = false;
        }

        private void SetWorkerPermission()
        {
            администрированиеToolStripMenuItem.Enabled = false;
        }

        private void Authorization()
        {
            LogInForm loginForm = new LogInForm(uow);
            loginForm.ShowDialog();
            User = loginForm.User;
            if (User != null)
            {
                if (User.Role != null)
                {
                    if (User.Role.Name == "Гость")
                    {
                        SetGuestPermission();
                    }
                    if (User.Role.Name == "Работник")
                    {
                        SetWorkerPermission();
                    }
                }
            }
            else
            {
                Close();
            }
        }

        private void ShowLicenseActivationForm()
        {
            ProtectionKeyForm protectionKeyForm = new ProtectionKeyForm();
            protectionKeyForm.ShowDialog();
            isActivatedLicense = protectionKeyForm.isActivated;
        }

        private void InitAdminAndRoles()
        {
            IUserService Service = new UserService(uow);
            var users = Service.GetAll();
            if (users.Count() == 0)
            {
                IRoleService roleService = new RoleService(uow);
                var roles = roleService.GetAll();
                if (roles.Count() < 3)
                {
                    BllRole adminRole = new BllRole
                    {
                        Name = "Администратор"
                    };
                    roleService.Create(adminRole);
                    BllRole workerRole = new BllRole
                    {
                        Name = "Работник"
                    };
                    roleService.Create(workerRole);
                    BllRole guestRole = new BllRole
                    {
                        Name = "Гость"
                    };
                    roleService.Create(guestRole);
                }
                var role = roleService.GetRoleByName("Администратор");
                BllUser admin = new BllUser
                {
                    Login = "admin",
                    Password = "admin",
                    Role = role,
                };
                Service.Create(admin);
            }
        }

        private void InitConfigs()
        {
            AppConfigManager configManager = new AppConfigManager();
            configManager.CreateTag(configManager.outputLocationTag, "C:\\");
            configManager.CreateTag(configManager.clearEquipmentAfterAdding, "false");
            configManager.CreateTag(configManager.clearDefectsAfterAdding, "false");
            configManager.CreateTag(configManager.clearEmployeesAfterAdding, "false");
            configManager.CreateTag(configManager.copyEmployeesToAllTypesOfMethods, "false");
            configManager.CreateTag(configManager.userIsReviewer, "false");
            configManager.CreateTag(configManager.hideControlMethods, "false");
            configManager.CreateTag(configManager.daysBeforeDeadline, "365");
        }

        private void ConnectToServer()
        {
            try
            {
                if (!isConnectedToServer)
                {
                    IControlNameService controlNameService = new ControlNameService(uow);
                    var controlNames = controlNameService.GetAll();
                    if (controlNames.Count() == 0)
                    {
                        controlNameService.Create(new BllControlName { Name = "ВИК" });
                        controlNames = controlNameService.GetAll();
                    }
                    if (controlNames.Count() < 2)
                    {
                        if (isFirstStart)
                        {
                            ShowLicenseActivationForm();
                        }

                        if (isActivatedLicense)
                        {
                            controlNameService.Create(new BllControlName { Name = "УЗК" });
                            controlNameService.Create(new BllControlName { Name = "РГК" });
                            controlNameService.Create(new BllControlName { Name = "ПВК" });
                            controlNames = controlNameService.GetAll();
                        }
                    }

                    InitAdminAndRoles();

                    ControlNames = new List<BllControlName>();
                    ControlMethodTabForms = new List<ControlMethodTabForm>();
                    foreach (var controlName in controlNames)
                    {
                        var tabForm = new ControlMethodTabForm(controlName.Name, uow, null);
                        tabForm.DisableFormControls();
                        ControlMethodTabForms.Add(tabForm);
                        tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
                        ControlNames.Add(controlName);
                    }

                    isConnectedToServer = true;
                    //RefreshDataGridUsingServer();
                    //Authorization();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка");
            }


        }

        private void SetCurrentControlsInControlMethodTabs(BllControlMethodsLib lib, BllJournal currentJournal)
        { 
            for (int i = 0; i < ControlNames.Count; i++)
            {
                foreach (var control in lib.Entities)
                {
                    if (ControlNames[i].Id == control.ControlName.Id)
                    {
                        ControlMethodTabForms[i].SetCurrentControlAndJournal(control, currentJournal, false);
                        ControlMethodTabForms[i].FillComponents();
                        ControlMethodTabForms[i].DisableFormControls();
                        break;
                    }
                }
            }

        }

        public void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        Stopwatch sw = new Stopwatch();
        private void StartDiagnostics()
        {
            sw.Start();
        }

        private void FinishDiagnostics(string message)
        {
            sw.Stop();
            //debugForm.AddNewEvent(message, (float)sw.Elapsed.TotalMilliseconds);
            sw.Reset();
        }

        private void FillRowUsingJournal(DataGridViewRow row, LiteJournal journal)
        {
            if (dataGridView1.Rows.IndexOf(row) == -1)
            {
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = dataGridView1.Rows.Count + 1;
            }
            row.Cells[1].Value = journal.RequestDate;
            row.Cells[2].Value = journal.ControlDate;
            row.Cells[3].Value = journal.RequestNumber;
            row.Cells[4].Value = journal.ComponentName != null ? journal.ComponentName : null;
            row.Cells[5].Value = journal.Amount;
            row.Cells[6].Value = journal.Size;
            row.Cells[7].Value = journal.WeldJointName != null ? journal.WeldJointName : null;
            row.Cells[8].Value = journal.WeldingType;
            row.Cells[9].Value = journal.MaterialName != null ? journal.MaterialName : null;
            row.Cells[10].Value = journal.ScheduleOrganizationName != null ? journal.ScheduleOrganizationName : null;
            const int numCell = 11;
            const int controlsCount = 4;
            for(int i = numCell; i < numCell + controlsCount; i++)
            {
                row.Cells[i].Value = "";
            }

            foreach (var control in journal.ControlMethods)
            {
                if (control.IsControlled != null)
                {
                    var control_id = control.Id;
                    if (control.IsControlled.Value)
                    {
                        row.Cells[numCell - 1 + control_id].Value = "  +";
                    }
                    else
                    {
                        row.Cells[numCell - 1 + control_id].Value = "  -";
                    }
                }
            }

        }

        public void AddRowToDataGrid(LiteJournal journal)
        {
            DataGridViewRow row = new DataGridViewRow();
            FillRowUsingJournal(row, journal);
            dataGridView1.Rows.Add(row);
            PutScrollBarDown();
        }

        public void AddNewJournal(LiteJournal journal)
        {
            Journals.Add(journal);
            AddRowToDataGrid(journal);
        }

        public void UpdateRowInDataGrid(BllJournal journal, int rowNumber)
        {
            var j = journalService.GetLiteJournal(journal);
            Journals[rowNumber] = j;
            DataGridViewRow row = dataGridView1.Rows[rowNumber];
            FillRowUsingJournal(row, j);
        }

        private void RefreshDataGridUsingServer()
        {
           // StartDiagnostics();

            IJournalService journalService = new JournalService(uow);
            var journals = journalService.GetAllLite().ToList();
            Journals = new List<LiteJournal>();

           // FinishDiagnostics("Get all journals");

            foreach (var journal in journals)
            {
                AddNewJournal(journal);
            }
            SetFirstRowSelected();
        }

        private void RefreshDataGrid()
        {
            //dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox2.Clear();
            foreach(ControlMethodTab tab in tabControl1.TabPages)
            {
                tab.tabForm.ClearData();
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (filtration(journalService.Get(Journals[row.Index].Id)))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
            SetFirstRowSelected();
        }

        private void SetFirstRowSelected()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Rows[0].Visible)
                {
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1_RowStateChanged(null, new DataGridViewRowStateChangedEventArgs(dataGridView1.Rows[0], DataGridViewElementStates.Selected));
                }
            }
        }

        
        private void сертификатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SertificateDirectoryForm sertificateForm = new SertificateDirectoryForm(uow);
            sertificateForm.ShowDialog(this);
            //RefreshData();
        }

        private void документацияМетодовКонтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlMethodDocumentationDirectoryForm controlMethodDocumentationForm = new ControlMethodDocumentationDirectoryForm(uow);
            controlMethodDocumentationForm.ShowDialog(this);
            //RefreshData();
        }

        private void заказчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerDirectoryForm customerForm = new CustomerDirectoryForm(uow);
            customerForm.ShowDialog(this);
           // RefreshData();
        }

        private void оборудованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EquipmentDirectoryForm equipmentForm = new EquipmentDirectoryForm(uow);
            equipmentForm.ShowDialog(this);
            //RefreshData();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialDirectoryForm materialForm = new MaterialDirectoryForm(uow);
            materialForm.ShowDialog(this);
            //RefreshData();
        }

        private void документацияТребованийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequirementDocumentationDirectoryForm requirementDocuemntationForm = new RequirementDocumentationDirectoryForm(uow);
            requirementDocuemntationForm.ShowDialog(this);
            //RefreshData();
        }

        private void сварочныеСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeldJointDirectoryForm weldJointForm = new WeldJointDirectoryForm(uow);
            weldJointForm.ShowDialog(this);
            //RefreshData();
        }

        private void шаблоныИМетодыКонтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateDirectoryForm templateForm = new TemplateDirectoryForm(uow);
            templateForm.ShowDialog(this);
           // RefreshData();
        }

        private void деталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComponentDirectoryForm directoryForm = new ComponentDirectoryForm(uow);
            directoryForm.ShowDialog(this);
            //RefreshData();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeDirectoryForm employeeForm = new EmployeeDirectoryForm(uow);
            employeeForm.ShowDialog(this);
            //RefreshData();
        }

        private void промышленныеОбъектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IndustrialObjectDirectoryForm industrialObjectForm = new IndustrialObjectDirectoryForm(uow);
            industrialObjectForm.ShowDialog(this);
            //RefreshData();
        }


        private void IncorrectDateMessage(int row, int column)
        {
            MessageBox.Show("Неверный вормат даты (требуется ДД.ММ.ГГГГ)", string.Format("Ошибка: строка {0}, столбец {1}", row + 1, column + 1));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            RefreshDataGridUsingServer();
        }

        const string dateFormat = "dd.MM.yyyy";

        private void FillContract(BllContract contract)
        {
            if (contract != null)
            {
                textBox3.Text = contract.Name;
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            if (!isAnyRowSelected() )
            {
                clearDataContainers();
                tabControl1.Enabled = false;
                return;
            }

            var selectedJournal = journalService.Get(Journals[dataGridView1.SelectedRows[0].Index].Id);

            if (selectedJournal.IndustrialObject != null)
            {
                textBox1.Text = selectedJournal.IndustrialObject.Name;
            }

            if (selectedJournal.Customer != null)
            {
                textBox2.Text = selectedJournal.Customer.Organization + " " + selectedJournal.Customer.Address + " " + selectedJournal.Customer.Phone;
            }

            FillContract(selectedJournal.Contract);
            richTextBox2.Text = selectedJournal.Description;

            SetCurrentControlsInControlMethodTabs(selectedJournal.ControlMethodsLib, selectedJournal);

            tabControl1.Enabled = true;

            tabControl1.Invalidate();

            if (selectedJournal.UserOwner != null)
            {
                label2.Text = selectedJournal.UserOwner.Login;
            }
            else
            {
                label2.Text = "-";
            }

            if (selectedJournal.UserModifierLogin != null)
            {
                label3.Visible = true;
                label4.Visible = true;
                label3.Text = selectedJournal.UserModifierLogin + " " + selectedJournal.ModifiedDate.Value.Date.ToString("dd.MM.yyyy");
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
            }

        }

        private void clearDataContainers()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Clear();
            richTextBox2.Text = "";
            if (ControlMethodTabForms != null)
            {
                foreach (var tab in ControlMethodTabForms)
                {
                    tab.ClearData();
                }
            }
        }



        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isAnyRowSelected() { return dataGridView1.SelectedRows.Count != 0; }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        delegate void ExportMethod(BllControlName controlName, List<BllJournal> journals, string folderPath);



        private void Connect(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document document = app.Documents.Open(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\help.docx");
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventForm eventForm = new EventForm(uow);
            eventForm.Show();
            eventForm.Focus();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tp = tabControl1.TabPages[e.Index];

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;  //optional

            // This is the rectangle to draw "over" the tabpage title
            RectangleF headerRect = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);

            // This is the default colour to use for the non-selected tabs
            SolidBrush sb = new SolidBrush(Color.AntiqueWhite);

            // This changes the colour if we're trying to draw the selected tabpage
            //if (tabControl1.SelectedIndex == e.Index)
            //    sb.Color = Color.Aqua;

            var form = ControlMethodTabForms[e.Index];

            if (form.IsControlled() == false)
            {
                sb.Color = Color.LightPink;
            }
            if (form.IsControlled() == true)
            {
                sb.Color = Color.LightGreen;
            }




            // Colour the header of the current tabpage based on what we did above
            g.FillRectangle(sb, e.Bounds);

            //Remember to redraw the text - I'm always using black for title text
            if (e.Index == tabControl1.SelectedIndex)
            {
                g.DrawString(tp.Text, tabControl1.Font, new SolidBrush(Color.Black), headerRect, sf);
            }
            else
            {
                g.DrawString(tp.Text, tabControl1.Font, new SolidBrush(Color.Brown), headerRect, sf);
            }
        }

        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (User == null)
            {
                Authorization();
            }
            if (User != null)
            {
                if (User.Role != null)
                {
                    if (User.Role.Name == "Администратор")
                    {
                        UserDirectoryForm form = new UserDirectoryForm(uow);
                        form.ShowDialog();
                    }
                }
            }

        }

        private void вверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFirstRow();
        }

        private void SelectFirstRow()
        {
            ClearSelectedRows();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void ClearSelectedRows()
        {
            DataGridViewSelectionMode oldmode = dataGridView1.SelectionMode;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.ClearSelection();

            dataGridView1.SelectionMode = oldmode;
        }

        private void внизДоУпораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectLastRow();
        }

        private void SelectLastRow()
        {
            ClearSelectedRows();
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                dataGridView1.Rows[count - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = count - 1;
            }
        }

        private void вверхToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    SelectLastRow();
                }
                else
                {
                    var rowToSelect = dataGridView1.SelectedRows[0].Index - 1;
                    if (rowToSelect > -1)
                    {
                        ClearSelectedRows();
                        dataGridView1.Rows[rowToSelect].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect;
                    }
                }
            }
        }

        private void внизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    SelectFirstRow();
                }
                else
                {
                    var rowToSelect = dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index + 1;
                    if (rowToSelect < count)
                    {
                        ClearSelectedRows();
                        dataGridView1.Rows[rowToSelect].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect;
                    }
                }
            }
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddJournalForm addJournalForm = new AddJournalForm(AddNewJournal, User, uow);
            addJournalForm.Show();
            
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                var j = journalService.Get(Journals[row.Index].Id);
                if (j.UserOwner == null || j.UserOwner.Id == User.Id || User.Role.Name == "Администратор")
                {
                    JournalForm changeJournalForm = new JournalForm(j, User, uow);
                    changeJournalForm.ShowDialog(this);
                    UpdateRowInDataGrid(changeJournalForm.Journal, row.Index);
                }
                else
                {
                    MessageBox.Show("Невозможно редактировать запись по объекту " + (Journals[row.Index].ComponentName != null ? Journals[row.Index].ComponentName : "<не указан>")  + ". Доступ запрещён.", "Оповещение");
                }
            }

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContinueChoiceForm form = new ContinueChoiceForm("Вы действительно хотите удалить запись?");
            form.ShowDialog();
            if (form.IsContinue)
            {
                var rows = dataGridView1.SelectedRows;
                List<LiteJournal> journalsForRemoving = new List<LiteJournal>();
                foreach (DataGridViewRow row in rows)
                {
                    var j = journalService.Get(Journals[row.Index].Id);
                    if (j.UserOwner == null || j.UserOwner.Id == User.Id)
                    {
                        journalsForRemoving.Add(Journals[row.Index]);
                        journalService.Delete(Journals[row.Index].Id);
                        dataGridView1.Rows.Remove(row);
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить запись по объекту " + (selectedJournal.Component != null ? selectedJournal.Component.Name : "<не указан>") + ". Доступ запрещён.", "Оповещение");
                    }
                }
                foreach (var journal in journalsForRemoving)
                {
                    Journals.Remove(journal);
                }
            }
        }

        Filtration filtrationForm = null;
        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filtrationForm == null)
            {
                filtrationForm = new Filtration(RefreshDataGrid, uow);
                filtration = filtrationForm.JournalFiltration;
            }
            filtrationForm.Show();

        }

        private void протоколыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportControlResultForm chooseControlNameForm = new ExportControlResultForm(uow);
            chooseControlNameForm.ShowDialog(this);
            List<BllJournal> SelectedJournals = new List<BllJournal>();


            if (chooseControlNameForm.SelectedControlName != null)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.Visible)
                    {
                        SelectedJournals.Add(journalService.Get(Journals[row.Index].Id));
                    }
                }
               
                
                saveFileDialog1.Filter = "Word files (*.docx)|*.docx";
                //exportMethod = ConvertManager.ConvertChosenControlResultsToExcel;
                

                int minProtocolNum = int.MaxValue, maxProtocolNum = -1;
                foreach (var journal in SelectedJournals)
                {
                    foreach (var control in journal.ControlMethodsLib.Entities)
                    {
                        if (control.ControlName.Id == chooseControlNameForm.SelectedControlName.Id)
                        {
                            if (control.ProtocolNumber.Value > maxProtocolNum)
                            {
                                maxProtocolNum = control.ProtocolNumber.Value ;
                            }
                            if (control.ProtocolNumber.Value < minProtocolNum)
                            {
                                minProtocolNum = control.ProtocolNumber.Value ;
                            }
                            

                        }
                    }
                }

                saveFileDialog1.FileName = chooseControlNameForm.SelectedControlName.Name + "№" + minProtocolNum.ToString() + "-" + maxProtocolNum.ToString();

                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    //exportMethod(chooseControlNameForm.SelectedControlName, SelectedJournals, saveFileDialog1.FileName);
                    WordDocumentManager wdm = new WordDocumentManager(uow);
                    try
                    {
                        wdm.CreateWordDocument(saveFileDialog1.FileName, SelectedJournals, User);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                }

            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            serviceDB = new ServiceDB();
            uow = new UnitOfWork(serviceDB);
            journalService = new JournalService(uow);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
            ConnectToServer();
            if (isConnectedToServer)
            {
                Authorization();
                RefreshDataGridUsingServer();
                EventForm eventForm = new EventForm(uow);
                eventForm.Show();
            }
            isFirstStart = false;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new System.Drawing.Font("Arial", 9F, GraphicsUnit.Pixel);
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.HeaderCell.Style = style;
            }
            PutScrollBarDown();
            if (User != null)
            {
                if (User.Employee != null)
                {
                    toolStripTextBox1.Text = "Исполнитель: " + User.Employee.Sirname + " " + User.Employee.Name[0] + "." + User.Employee.Fathername[0] + ".";
                }
                else
                {
                    if (User.Role.Name == "Гость")
                    {
                        toolStripTextBox1.Text = "Гостевой допуск";
                    }
                    else
                    {
                        toolStripTextBox1.Text = "Исполнитель: <не указан>";
                    }
                }
            }

            SetOutputLocation();
            AppConfigManager configManager = new AppConfigManager();
            if (bool.Parse(configManager.GetTagValue(configManager.hideControlMethods)))
            {
                HideControlMethodTab();
            }
            //debugForm = new DebugForm();
            //debugForm.Show();
        }

        public void SetOutputLocation()
        {
            AppConfigManager appConfigManager = new AppConfigManager();
            saveFileDialog1.InitialDirectory = appConfigManager.GetOutputLocation();
        }

        const int dataGridWidth = 605;
        const int tabWidth = 343;

        public void HideControlMethodTab()
        {
            if (dataGridView1.Width == dataGridWidth)
            {
                dataGridView1.Width += tabWidth;
                groupBox4.Width += tabWidth;
                tabControl1.Width = 0;
            }

        }

        public void ShowControlMethodTab()
        {
            if (dataGridView1.Width != dataGridWidth)
            {
                dataGridView1.Width = dataGridWidth;
                tabControl1.Width = tabWidth;
                groupBox4.Width -= tabWidth;
            }
        }

        private void журналОбъектовPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateRangeForm form = new DateRangeForm();
            form.ShowDialog();
            if (!form.isCanceled)
            {
                List<BllJournal> selectedJournal = new List<BllJournal>();
                var left = form.left.Date;
                var right = form.right.Date;
                foreach (var journal in Journals)
                {
                    if (journal.ControlDate.Value.Date.CompareTo(left) >= 0 && journal.ControlDate.Value.Date.CompareTo(right) <= 0)
                    {
                        selectedJournal.Add(journalService.Get(journal.Id));
                    }
                }
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
                saveFileDialog1.FileName = "Журнал";
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    ConvertManager.WriteJournalToExcel(selectedJournal, saveFileDialog1.FileName);
                }
            }
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Settings form = new Settings(this);
            form.ShowDialog();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ServiceInfo form = new ServiceInfo();
                form.ShowDialog();
            }
        }

        private void сырьёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RawDirectoryForm form = new RawDirectoryForm(uow);
            form.ShowDialog();
        }

        private void организацииВыпЧертежиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleOrganizationDirectoryForm form = new ScheduleOrganizationDirectoryForm(uow);
            form.ShowDialog();
        }

        bool isSearchUsing = false;
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            List<string> fields = new List<string>();
            if (isSearchUsing)
            {
                for (int i = 0; i < Journals.Count; i++)
                {
                    var j = Journals.ElementAt(i);
                    if (j.ComponentName != null)
                    {
                        fields.Add(j.ComponentName);
                    }
                    if (j.Amount != null)
                    {
                        fields.Add(j.Amount.ToString());
                    }
                    if (j.ContractName != null)
                    {
                        fields.Add(j.ContractName);
                    }
                    if (j.ControlDate != null)
                    {
                        fields.Add(j.ControlDate.Value.ToString(dateFormat));
                    }
                    if (j.MaterialName != null)
                    {
                        fields.Add(j.MaterialName);
                    }
                    if (j.RequestDate != null)
                    {
                        fields.Add(j.RequestDate.Value.ToString(dateFormat));
                    }
                    if (j.RequestNumber != null)
                    {
                        fields.Add(j.RequestNumber.ToString());
                    }
                    if (j.ScheduleOrganizationName != null)
                    {
                        fields.Add(j.ScheduleOrganizationName);
                    }
                    if (j.Size != null)
                    {
                        fields.Add(j.Size);
                    }
                    if (j.WeldingType != null)
                    {
                        fields.Add(j.WeldingType);
                    }
                    if (j.WeldJointName != null)
                    {
                        fields.Add(j.WeldJointName);
                    }
                    bool isFound = false;
                    foreach(var item in fields)
                    {
                        if (item.IndexOf(textBox4.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            dataGridView1.Rows[i].Visible = true;
                            isFound = true;
                            break;
                        }
                    }
                    if (isFound == false)
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }

                }
            }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            isSearchUsing = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isSearchUsing = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
               dataGridView1.Rows[i].Visible = true;
            }
            textBox4.Text = "Поиск...";
        }
    }
}
