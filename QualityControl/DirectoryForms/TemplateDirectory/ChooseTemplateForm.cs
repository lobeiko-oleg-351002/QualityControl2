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


namespace QualityControl_Server.Forms.TemplateDirectory
{
    public partial class ChooseTemplateForm : DirectoryForm
    {
        List<BllTemplate> Templates;
        BllTemplate template;
        public ChooseTemplateForm(IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }
        public ChooseTemplateForm() : base()
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
                    foreach (BllControl control in Template.ControlMethodsLib.Entities)
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

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
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
                template = Templates[rows[0].Index];
                this.Close();
            }
        }

        public BllTemplate GetChosenTemplate()
        {
            return template;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            template = Templates[rows[0].Index];
            this.Close();
        }


    }
}
