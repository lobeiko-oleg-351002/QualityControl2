using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.DirectoryForms.ScheduleOrganizationDirectory
{
    public partial class ChooseScheduleOrganizationForm : DirectoryForm
    {
        List<BllScheduleOrganization> ScheduleOrganizations;
        BllScheduleOrganization ScheduleOrganization;

        public ChooseScheduleOrganizationForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public ChooseScheduleOrganizationForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
            ScheduleOrganizations = Service.GetAll().ToList();
            const string dateFormat = "dd.MM.yyyy";
            foreach (var ScheduleOrganization in ScheduleOrganizations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = ScheduleOrganization.Name;
                row.Cells[1].Value = ScheduleOrganization.Address;
                row.Cells[2].Value = ScheduleOrganization.Description;

                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddScheduleOrganizationForm AddScheduleOrganizationForm = new AddScheduleOrganizationForm(this, uow);
            AddScheduleOrganizationForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(ScheduleOrganizations[row.Index].Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeScheduleOrganizationForm changeScheduleOrganizationForm = new ChangeScheduleOrganizationForm(this, ScheduleOrganizations[rowsList[i].Index], uow);
                changeScheduleOrganizationForm.ShowDialog(this);
            }
            RefreshData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                ScheduleOrganization = ScheduleOrganizations[rows[0].Index];
                this.Close();
            }
        }

        public BllScheduleOrganization GetChosenScheduleOrganization()
        {
            return ScheduleOrganization;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            ScheduleOrganization = ScheduleOrganizations[rows[0].Index];
            this.Close();
        }
    }
}
