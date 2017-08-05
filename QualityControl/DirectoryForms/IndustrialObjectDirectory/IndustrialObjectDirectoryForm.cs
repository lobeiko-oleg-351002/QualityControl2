using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.Forms.IndustrialObjectDirectory
{
    public partial class IndustrialObjectDirectoryForm : DirectoryForm
    {
        IEnumerable<DalIndustrialObject> IndustrialObjects;

        public IndustrialObjectDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            RefreshData();
        }
        public IndustrialObjectDirectoryForm() : base()
        {

        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            //IIndustrialObjectService Service = new IndustrialObjectService(uow);
            IndustrialObjects = uow.IndustrialObjects.GetAll();
            //IComponentService componentService = new ComponentService(uow);
            foreach (var IndustrialObject in IndustrialObjects)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = IndustrialObject.Name;
                var components = uow.Components.GetComponentsByIndustrialObject(IndustrialObject.Id);
                foreach (var component in components)
                {
                    ((DataGridViewComboBoxCell)row.Cells[1]).Items.Add(component.Name);
                }
                if (components.Count() != 0)
                {
                    ((DataGridViewComboBoxCell)row.Cells[1]).Value = ((DataGridViewComboBoxCell)row.Cells[1]).Items[0];
                }

                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddIndustrialObjectForm AddIndustrialObjectForm = new AddIndustrialObjectForm(this, uow);
            AddIndustrialObjectForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IIndustrialObjectService Service = new IndustrialObjectService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(IndustrialObjects.ElementAt(row.Index).Id);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IIndustrialObjectService Service = new IndustrialObjectService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeIndustrialObjectForm changeIndustrialObjectForm = new ChangeIndustrialObjectForm(this, Service.Get(IndustrialObjects.ElementAt(rowsList[i].Index).Id), uow);
                changeIndustrialObjectForm.ShowDialog(this);
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
    }
}
