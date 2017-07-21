using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.DirectoryForms.RawDirectory
{
    public partial class RawDirectoryForm : DirectoryForm
    {
        List<BllRaw> Raws;

        public RawDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IRawService Service = new RawService(uow);
            Raws = Service.GetAll().ToList();
            foreach (var Raw in Raws)
            {
                AddRow(Raw);
            }
                        ((DataGridViewImageColumn)dataGridView1.Columns[5]).ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void AddRow(BllRaw Raw)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);
            row.Cells[0].Value = Raw.Name;
            row.Cells[1].Value = Raw.DeliveryDate;
            row.Cells[2].Value = Raw.Documentation;
            row.Cells[3].Value = Raw.IsValid ? "Годен" : "Не годен";
            row.Cells[4].Value = Raw.Certificate;
            row.Cells[5].Value = Raw.CertificateImage;

            dataGridView1.Rows.Add(row);
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddRawForm addRawForm = new AddRawForm(this, uow);
            addRawForm.ShowDialog(this);

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Raws.RemoveAt(row.Index);
            }
            // RefreshData();
        }


        protected override void button3_Click(object sender, EventArgs e)
        {
            IRawService Service = new RawService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeRawForm changeRawForm = new ChangeRawForm(uow, this, Raws[rowsList[i].Index]);
                changeRawForm.ShowDialog(this);
            }
            RefreshData();
        }

        public override void SelectRow(IBllEntity entity)
        {
            dataGridView1.ClearSelection();
            var id = Raws.FindIndex(Raw => Raw.Id == entity.Id);
            if (id > -1) dataGridView1.Rows[id].Selected = true;
        }



        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = image;
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(img, loc);
        }

        Image image;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += PrintPage;
                image = byteArrayToImage(Raws[e.RowIndex].CertificateImage);
                pd.Print();
            }
        }

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
        }
    }
}