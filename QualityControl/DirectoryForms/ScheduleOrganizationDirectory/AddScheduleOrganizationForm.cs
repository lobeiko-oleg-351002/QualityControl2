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

namespace QualityControl_Server.DirectoryForms.ScheduleOrganizationDirectory
{
    public partial class AddScheduleOrganizationForm : AddForm
    {
        public AddScheduleOrganizationForm() : base()
        {
            InitializeComponent();
        }

        public AddScheduleOrganizationForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            this.uow = uow;
            InitializeComponent();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название организации", "Оповещение");
            }
            else
            {
                BllScheduleOrganization ScheduleOrganization = new BllScheduleOrganization
                {
                    Name = textBox1.Text,
                    Address = textBox2.Text,
                    Description = textBox3.Text,
                };
                IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
                Service.Create(ScheduleOrganization);
                base.button2_Click(sender, e);
            }
        }

    }
}
