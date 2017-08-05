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
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using BLL.Services;
using DAL.Entities;

namespace QualityControl_Server.Forms.ComponentDirectory
{
    public partial class ChooseComponentForm : DirectoryForm
    {
        private void button5_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                Component = Components.ElementAt(rows[0].Index);
                this.Close();
            }

        }

        IEnumerable<DalComponent> Components;
        DalComponent Component;
        public ChooseComponentForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            Components = uow.Components.GetAll();
            Dictionary<int, DalTemplate> templates = new Dictionary<int, DalTemplate>();
            Dictionary<int, DalIndustrialObject> industrialObjects = new Dictionary<int, DalIndustrialObject>();
            foreach (var Component in Components)
            {
                DalTemplate t = null;
                if (Component.Template_id != null)
                {
                    if (!templates.ContainsKey(Component.Template_id.Value))
                    {
                        templates.Add(Component.Template_id.Value, uow.Templates.Get(Component.Template_id.Value));
                    }
                    t = templates[Component.Template_id.Value];
                }

                DalIndustrialObject io = null;
                if (Component.IndustrialObject_id != null)
                {
                    if (!industrialObjects.ContainsKey(Component.IndustrialObject_id.Value))
                    {
                        industrialObjects.Add(Component.IndustrialObject_id.Value, uow.IndustrialObjects.Get(Component.IndustrialObject_id.Value));
                    }
                    io = industrialObjects[Component.IndustrialObject_id.Value];
                }

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Component.Name;
                row.Cells[1].Value = Component.Pressmark;
                row.Cells[2].Value = t != null ? t.Name : "<отсутствует>";
                row.Cells[3].Value = io != null ? io.Name : "<не указан>";
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
            IComponentService Service = new ComponentService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Components.ElementAt(row.Index).Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IComponentService Service = new ComponentService(uow);
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

        public BllComponent GetChosenComponent()
        {
            IComponentService Service = new ComponentService(uow);
            return Service.Get(Component.Id);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            Component = Components.ElementAt(rows[0].Index);
            this.Close();
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
