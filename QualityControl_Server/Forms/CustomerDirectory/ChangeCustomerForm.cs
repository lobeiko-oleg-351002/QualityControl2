﻿using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class ChangeCustomerForm : ChangeForm
    {
        UilCustomer oldCustomer;
        public ChangeCustomerForm() : base()
        {
            InitializeComponent();
        }

        public ChangeCustomerForm(DirectoryForm parent, UilCustomer oldCustomer, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldCustomer = oldCustomer;
            textBox1.Text = oldCustomer.Organization;
            textBox2.Text = oldCustomer.Address;
            textBox3.Text = oldCustomer.Phone;
            textBox4.Text = oldCustomer.Contract;
            dateTimePicker1.Value = (DateTime)oldCustomer.ContractBeginDate;
            dateTimePicker2.Value = (DateTime)oldCustomer.ContractEndDate;

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
                oldCustomer.Contract = textBox4.Text;
                oldCustomer.ContractBeginDate = dateTimePicker1.Value;
                oldCustomer.ContractEndDate = dateTimePicker2.Value;

                ICustomerRepository repository = ServiceChannelManager.Instance.CustomerRepository;
                repository.Update(oldCustomer);
                base.button2_Click(sender, e);
            }
        }
    }
}
