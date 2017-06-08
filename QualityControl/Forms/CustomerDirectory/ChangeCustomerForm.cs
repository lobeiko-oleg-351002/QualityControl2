
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
using QualityControl_Server.Forms.ContractDirectory;

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class ChangeCustomerForm : ChangeForm
    {
        BllCustomer oldCustomer;
        public ChangeCustomerForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        BllContractLib contractLib;
        public ChangeCustomerForm(DirectoryForm parent, BllCustomer oldCustomer, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldCustomer = oldCustomer;
            textBox1.Text = oldCustomer.Organization;
            textBox2.Text = oldCustomer.Address;
            textBox3.Text = oldCustomer.Phone;
            contractLib = oldCustomer.ContractLib;
            if (oldCustomer.ContractLib != null)
            {
                foreach (var Contract in oldCustomer.ContractLib.Contract)
                {
                    listBox1.Items.Add(Contract.Name);
                }
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название организации", "Оповещение");
            }
            else
            {
                oldCustomer.Organization = textBox1.Text;
                oldCustomer.Address = textBox2.Text;
                oldCustomer.Phone = textBox3.Text;
                oldCustomer.ContractLib = contractLib;

                ICustomerService Service = new CustomerService(uow);
                Service.Update(oldCustomer);
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
                oldCustomer.ContractLib.Contract.Add(Contract);
                listBox1.Items.Add(Contract.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            oldCustomer.ContractLib.Contract.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i != -1)
            {
                ChangeContractForm ContractForm = new ChangeContractForm(oldCustomer.ContractLib.Contract[i], uow);
                ContractForm.ShowDialog(this);

                BllContract Contract = ContractForm.oldContract;
                if (Contract != null)
                {
                    oldCustomer.ContractLib.Contract[i] = Contract;
                    listBox1.Items[i] = Contract.Name;
                }
            }
        }
    }
}
