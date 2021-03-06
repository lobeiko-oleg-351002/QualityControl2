﻿using BLL.Entities;
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


namespace QualityControl_Client.Forms.TemplateDirectory
{
    public partial class TemplateDirectoryForm : DirectoryForm
    {
        List<BllTemplate> Templates;
        IUnitOfWork uow;
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
            Templates = Service.GetAll().ToList();
            foreach (var Template in Templates)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Template.Name;
                row.Cells[1].Value = Template.Material != null ? Template.Material.Name : "";
                row.Cells[2].Value = Template.WeldJoint != null ? Template.WeldJoint.Name : "";

                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)row.Cells[3];
                if (Template.ControlMethodsLib != null)
                {
                    foreach (BllControl control in Template.ControlMethodsLib.Control)
                    {
                        comboBoxCell.Items.Add(control.ControlName.Name);
                    }
                    if (comboBoxCell.Items.Count > 0)
                    {
                        comboBoxCell.Value = comboBoxCell.Items[0];
                    }
                }

                row.Cells[4].Value = Template.Description;
                row.Cells[5].Value = Template.IndustrialObject != null ? Template.IndustrialObject.Name : "";
                row.Cells[6].Value = Template.Customer != null ? Template.Customer.Organization : "";
                row.Cells[7].Value = Template.Size;
                row.Cells[8].Value = Template.WeldingType;

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
                Service.Delete(Templates[row.Index]);
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
                ChangeTemplateForm changeTemplateForm = new ChangeTemplateForm(this, Templates[rowsList[i].Index], uow);
                changeTemplateForm.ShowDialog(this);
            }
            RefreshData();
        }

    }
}
