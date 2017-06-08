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
    public partial class AddContractForm : AddForm
    {
        public AddContractForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddContractForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }
        public BllContract Contract { get; private set; }
        protected override void button2_Click(object sender, EventArgs e)
        {
            Contract = new BllContract
            {
                Name = textBox1.Text,
                BeginDate = dateTimePicker1.Value,
                EndDate = dateTimePicker2.Value
            };
           // IContractService Service = new ContractService(uow);

            string errorMessage = "Заголовок не указан";
            if (Contract.Name == "")
            {
                MessageBox.Show(errorMessage, "Оповещение");
            }
            Close();

        }

    }
 }
