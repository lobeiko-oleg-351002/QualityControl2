using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client;
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
    public partial class ContractDirectoryForm : DirectoryForm
    {
        List<BllContract> Contracts;
        IUnitOfWork uow;
        public ContractDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
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

        private void AddItemToDataGrid(BllContract contract)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);
            row.Cells[0].Value = contract.Name;
            row.Cells[1].Value = contract.BeginDate;
            row.Cells[2].Value = contract.EndDate;
            dataGridView1.Rows.Add(row);
            Contracts.Add(contract);
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddContractForm addContractForm = new AddContractForm(this, uow);
            addContractForm.ShowDialog(this);
            AddItemToDataGrid(addContractForm.Contract);

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Contracts.RemoveAt(row.Index);
            }
           // RefreshData();
        }

        private void RefreshRow(BllContract contract, int rowNumber)
        {
            Contracts[rowNumber] = contract;
            dataGridView1.Rows[rowNumber].Cells[0].Value = contract.Name;
            dataGridView1.Rows[rowNumber].Cells[1].Value = contract.BeginDate;
            dataGridView1.Rows[rowNumber].Cells[2].Value = contract.EndDate;
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
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
                RefreshRow(changeContractForm.oldContract, rowsList[i].Index);
            }
            //RefreshData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
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
            var id = Contracts.FindIndex(Contract => Contract.Id == entity.Id);
            if (id > -1) dataGridView1.Rows[id].Selected = true;
        }
    }
}
