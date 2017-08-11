using BLL.Entities;
using BLL.Entities.Interface;
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
    public partial class ScheduleOrganizationDirectoryForm : DirectoryForm
    {
        List<BllScheduleOrganization> ScheduleOrganizations;

        public ScheduleOrganizationDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
            ScheduleOrganizations = Service.GetAll().ToList();
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

        public override void SelectRow(IBllEntity entity)
        {
            dataGridView1.ClearSelection();
            var id = ScheduleOrganizations.FindIndex(ScheduleOrganization => ScheduleOrganization.Id == entity.Id);
            if (id > -1) dataGridView1.Rows[id].Selected = true;
        }
    }

}
