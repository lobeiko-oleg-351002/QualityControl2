using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms.ComponentDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server.Forms.IndustrialObjectDirectory
{
    public partial class ChangeIndustrialObjectForm : ChangeForm
    {
        BllIndustrialObject oldIndustrialObject;

        public ChangeIndustrialObjectForm() : base()
        {
            InitializeComponent();
        }


        public ChangeIndustrialObjectForm(DirectoryForm parent, BllIndustrialObject oldIndustrialObject, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.oldIndustrialObject = oldIndustrialObject;
            this.uow = uow;
           
            textBox1.Text = oldIndustrialObject.Name;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldIndustrialObject.Name = textBox1.Text;


                IIndustrialObjectService Service = new IndustrialObjectService(uow);
                Service.Update(oldIndustrialObject);
                base.button2_Click(sender, e);
            }
        }

       
    }
}
