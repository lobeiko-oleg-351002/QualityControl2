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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.DirectoryForms.RawDirectory
{
    public partial class AddRawForm : AddForm
    {
        public AddRawForm() : base()
        {
            InitializeComponent();
        }

        public AddRawForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllRaw Raw = new BllRaw
                {
                    Name = textBox1.Text,
                    DeliveryDate = dateTimePicker1.Value,
                    Documentation =textBox2.Text,
                    Certificate = textBox3.Text,
                    IsValid = checkBox1.Checked,
                    CertificateImage = pictureBox1.Image != null ? imageToByteArray(Image.FromFile(openFileDialog1.FileName)) : null,
                };
                IRawService Service = new RawService(uow);
                Service.Create(Raw);
                base.button2_Click(sender, e);
            }
        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
