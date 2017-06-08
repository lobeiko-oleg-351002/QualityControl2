using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.Forms.ContractDirectory
{
    public partial class ChangeContractForm : ChangeForm
    {
        public BllContract oldContract;
        public ChangeContractForm() : base()
        {
            InitializeComponent();
        }

        IUnitOfWork uow;

        public ChangeContractForm(BllContract oldContract) : base()
        {
            InitializeComponent();
            InitControls(oldContract);
        }

        private void InitControls(BllContract oldContract)
        {
            this.oldContract = oldContract;

            textBox1.Text = oldContract.Name;

            dateTimePicker1.Value = oldContract.BeginDate.Value;
            dateTimePicker2.Value = oldContract.EndDate.Value;
        }

        public ChangeContractForm(BllContract oldContract, IUnitOfWork uow) : base()
        {
            InitializeComponent();
            InitControls(oldContract);
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            oldContract.Name = textBox1.Text;
            oldContract.BeginDate = dateTimePicker1.Value;
            oldContract.EndDate = dateTimePicker2.Value;

            string errorMessage = "Заголовок не указан";
            if (oldContract.Name == "")
            {
                MessageBox.Show(errorMessage, "Оповещение");
            }
            else
            {
                if (oldContract.Id != 0)
                {
                    IContractService Service = new ContractService(uow);
                    Service.Update(oldContract);
                }
            }
            Close();
        }
    }
}