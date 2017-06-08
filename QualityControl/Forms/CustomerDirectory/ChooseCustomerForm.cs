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
using DAL.Repositories.Interface;
using BLL.Entities;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class ChooseCustomerForm : DirectoryForm
    {
        List<BllCustomer> Customers;
        BllCustomer customer;
        IUnitOfWork uow;
        public ChooseCustomerForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public ChooseCustomerForm() : base()
        {
            InitializeComponent();
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
                foreach (var contract in Customer.ContractLib.Contract)
                {
                    ((DataGridViewComboBoxCell)row.Cells[4]).Items.Add(contract.Name + " " + contract.BeginDate.Value.ToString(dateFormat) + "-" + contract.EndDate.Value.ToString(dateFormat));
                }
                if (Customer.ContractLib.Contract.Count != 0)
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
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                customer = Customers[rows[0].Index];
                this.Close();
            }
        }

        public BllCustomer GetChosenCustomer()
        {
            return customer;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            customer = Customers[rows[0].Index];
            this.Close();
        }
    }

}
