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
    public partial class ChangeRawForm : ChangeForm
    {
        BllRaw oldRaw;


        public ChangeRawForm() : base()
        {
            InitializeComponent();
        }

        public ChangeRawForm(IUnitOfWork uow, DirectoryForm parent, BllRaw oldRaw) : base(parent)
        {
            InitializeComponent();
            this.oldRaw = oldRaw;
            this.uow = uow;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            textBox1.Text = oldRaw.Name;
            textBox2.Text = oldRaw.Documentation;
            textBox3.Text = oldRaw.Certificate;
            checkBox1.Checked = oldRaw.IsValid;
            dateTimePicker1.Value = oldRaw.DeliveryDate.Date;
            pictureBox1.Image = byteArrayToImage(oldRaw.CertificateImage);

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldRaw.Name = textBox1.Text;
                oldRaw.Documentation = textBox2.Text;
                oldRaw.CertificateImage = openFileDialog1.FileName != "" ? imageToByteArray(Image.FromFile(openFileDialog1.FileName)) : oldRaw.CertificateImage;
                oldRaw.Certificate = textBox3.Text;
                oldRaw.IsValid = checkBox1.Checked;
                IRawService Service = new RawService(uow);
                Service.Update(oldRaw);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
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

        private Image byteArrayToImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new MemoryStream(array))
                {
                    return Image.FromStream(ms);
                }
            }
            return null;
        }
    }
}
