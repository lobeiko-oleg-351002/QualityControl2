﻿
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
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Server.Forms.RequirementDocumentationDirectory
{
    public partial class ChooseRequirementDocumentationForm : DirectoryForm
    {
        List<BllRequirementDocumentation> RequirementDocumentations;
        BllRequirementDocumentation requirementDocumentation;

        public ChooseRequirementDocumentationForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();

        }
        public ChooseRequirementDocumentationForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            RequirementDocumentations = Service.GetAll().ToList();
            foreach (var RequirementDocumentation in RequirementDocumentations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = RequirementDocumentation.Name;
                row.Cells[1].Value = RequirementDocumentation.Pressmark;
                row.Cells[2].Value = RequirementDocumentation.ObjectGroup;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddRequirementDocumentationForm AddRequirementDocumentationForm = new AddRequirementDocumentationForm(this, uow);
            AddRequirementDocumentationForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(RequirementDocumentations[row.Index].Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeRequirementDocumentationForm changeRequirementDocumentationForm = new ChangeRequirementDocumentationForm(this, RequirementDocumentations[rowsList[i].Index], uow);
                changeRequirementDocumentationForm.ShowDialog(this);
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
                requirementDocumentation = RequirementDocumentations[rows[0].Index];
                this.Close();
            }
        }

        public BllRequirementDocumentation GetChosenRequirementDocumentation()
        {
            return requirementDocumentation;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            requirementDocumentation = RequirementDocumentations[rows[0].Index];
            this.Close();
        }
    }
}
