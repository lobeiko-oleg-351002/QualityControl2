using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.Forms.ComponentDirectory
{
    public partial class ComponentDirectoryForm : DirectoryForm
    {
        List<LiteComponent> Components;
        IComponentService Service;

        public ComponentDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            Service = new ComponentService(uow);
            RefreshData();
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            Components = Service.GetAllLite();

            foreach (var Component in Components)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Component.Name;
                row.Cells[1].Value = Component.Pressmark;
                row.Cells[2].Value = Component.TemplateName != null ? Component.TemplateName : "<отсутствует>";
                row.Cells[3].Value = Component.IndustrialObjectName != null ? Component.IndustrialObjectName : "<не указан>";
                row.Cells[4].Value = Component.Count;
                row.Cells[5].Value = Component.Description;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddComponentForm AddComponentForm = new AddComponentForm(this, uow);
            AddComponentForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Components.ElementAt(row.Index).Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeComponentForm changeComponentForm = new ChangeComponentForm(this, Service.Get(Components.ElementAt(rowsList[i].Index).Id), uow);
                changeComponentForm.ShowDialog(this);
            }
            RefreshData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertDataGridToPdf(dataGridView1, saveFileDialog1.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog2.FileName = "";
            if (DialogResult.OK == saveFileDialog2.ShowDialog())
            {                
                ConvertManager.ConvertDataGridToExcel(dataGridView1, saveFileDialog2.FileName);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Components.Count(); i++)
            {
                var item = Components.ElementAt(i).Pressmark;
                if (item.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    dataGridView1.Rows[i].Visible = false;
                }
            }
        }
    }
}
