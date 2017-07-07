
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
using BLL.Entities.Interface;

namespace QualityControl_Server.Forms.CustomerDirectory
{
    public partial class CustomerDirectoryForm : DirectoryForm
    {
        List<BllCustomer> Customers;

        public CustomerDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ICustomerService Service = new CustomerService(uow);
            Customers = Service.GetAll().ToList();
            int num = 0;
            const string dateFormat = "dd.MM.yyyy";
            foreach (var Customer in Customers)
            {
                num++;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = num;
                row.Cells[1].Value = Customer.Organization;
                row.Cells[2].Value = Customer.Address;
                row.Cells[3].Value = Customer.Phone;
                foreach(var contract in Customer.ContractLib.Entities)
                {
                    ((DataGridViewComboBoxCell)row.Cells[4]).Items.Add(contract.Name + " " + contract.BeginDate.Value.ToString(dateFormat) + " - " + contract.EndDate.Value.ToString(dateFormat));
                }
                if (Customer.ContractLib.Entities.Count != 0)
                {
                    ((DataGridViewComboBoxCell)row.Cells[4]).Value = ((DataGridViewComboBoxCell)row.Cells[4]).Items[0];
                }

                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddCustomerForm AddCustomerForm = new AddCustomerForm(this, uow);
            AddCustomerForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ICustomerService Service = new CustomerService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Customers[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ICustomerService Service = new CustomerService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeCustomerForm changeCustomerForm = new ChangeCustomerForm(this, Customers[rowsList[i].Index], uow);
                changeCustomerForm.ShowDialog(this);
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

        public override void SelectRow(IBllEntity entity)
        {
            dataGridView1.ClearSelection();
            var id = Customers.FindIndex(customer => customer.Id == entity.Id);
            if (id > -1) dataGridView1.Rows[id].Selected = true;
        }
    }

}