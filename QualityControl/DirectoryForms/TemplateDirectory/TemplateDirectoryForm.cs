using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

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
    public partial class TemplateDirectoryForm : DirectoryForm
    {
        List<LiteTemplate> Templates;

        public TemplateDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }
        public TemplateDirectoryForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ITemplateService Service = new TemplateService(uow);
            Templates = Service.GetAllLite().ToList();
            foreach (var Template in Templates)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Template.Name;
                row.Cells[1].Value = Template.MaterialName != null ? Template.MaterialName : "";
                row.Cells[2].Value = Template.ScheduleOrganizationName != null ? Template.ScheduleOrganizationName : "";

                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)row.Cells[3];
                foreach (string name in Template.ControlMethods)
                {
                    comboBoxCell.Items.Add(name);
                }
                if (comboBoxCell.Items.Count > 0)
                {
                    comboBoxCell.Value = comboBoxCell.Items[0];
                }

                row.Cells[4].Value = Template.Description;
                row.Cells[5].Value = Template.IndustrialObjectName != null ? Template.IndustrialObjectName : "";
                row.Cells[6].Value = Template.CustomerName != null ? Template.CustomerName : "";
                row.Cells[7].Value = Template.Size;

                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddTemplateForm AddTemplateForm = new AddTemplateForm(this, uow);
            AddTemplateForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ITemplateService Service = new TemplateService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Templates[row.Index].Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ITemplateService Service = new TemplateService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeTemplateForm changeTemplateForm = new ChangeTemplateForm(this, Service.Get(Templates[rowsList[i].Index].Id), uow);
                changeTemplateForm.ShowDialog(this);
            }
            RefreshData();
        }

    }
}
