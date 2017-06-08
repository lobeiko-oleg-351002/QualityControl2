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
using QualityControl_Server.Forms.ContractDirectory;

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class AddCustomerForm : AddForm
    {
        BllContractLib ContractLib;
        public AddCustomerForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddCustomerForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            this.uow = uow;
            InitializeComponent();
            ContractLib = new BllContractLib();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название организации", "Оповещение");
            }
            else
            {
                BllCustomer customer = new BllCustomer
                {
                    Organization = textBox1.Text,
                    Address = textBox2.Text,
                    Phone = textBox3.Text,
                    ContractLib = ContractLib
                };
                ICustomerService Service = new CustomerService(uow);
                Service.Create(customer);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddContractForm ContractForm = new AddContractForm();
            ContractForm.ShowDialog(this);
            BllContract Contract = ContractForm.Contract;
            if (Contract != null)
            {
                ContractLib.Contract.Add(Contract);
                listBox1.Items.Add(Contract.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ContractLib.Contract.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i != -1)
            {
                ChangeContractForm ContractForm = new ChangeContractForm(ContractLib.Contract[i]);
                ContractForm.ShowDialog(this);

                BllContract Contract = ContractForm.oldContract;
                if (Contract != null)
                {
                    ContractLib.Contract[i] = Contract;
                    listBox1.Items[i] = Contract.Name;
                }
            }
        }
    }
}
