using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms;
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
    public partial class ChooseContractForm : DirectoryForm
    {
        List<BllContract> Contracts;
        BllContract Contract;

        public ChooseContractForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public ChooseContractForm() : base()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IContractService Service = new ContractService(uow);
            Contracts = Service.GetAll().ToList();
            foreach (var Contract in Contracts)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Contract.Name;
                row.Cells[1].Value = Contract.BeginDate;
                row.Cells[2].Value = Contract.EndDate;

                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddContractForm AddContractForm = new AddContractForm(this, uow);
            AddContractForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IContractService Service = new ContractService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Contracts[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IContractService Service = new ContractService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeContractForm changeContractForm = new ChangeContractForm(Contracts[rowsList[i].Index]);
                changeContractForm.ShowDialog(this);
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
                Contract = Contracts[rows[0].Index];
                this.Close();
            }
        }

        public BllContract GetChosenContract()
        {
            return Contract;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            Contract = Contracts[rows[0].Index];
            this.Close();
        }
    }
}
