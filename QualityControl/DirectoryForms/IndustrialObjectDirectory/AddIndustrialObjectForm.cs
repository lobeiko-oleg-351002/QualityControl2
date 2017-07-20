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
    public partial class AddIndustrialObjectForm : AddForm
    {


        public AddIndustrialObjectForm() : base()
        {
            InitializeComponent();
        }

        public AddIndustrialObjectForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
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
                
                BllIndustrialObject IndustrialObject = new BllIndustrialObject
                {
                    Name = textBox1.Text,
                    //ComponentLib = ComponentLib
                };
                IIndustrialObjectService Service = new IndustrialObjectService(uow);
                Service.Create(IndustrialObject);
                base.button2_Click(sender, e);
            }
        }

       
    }
}
