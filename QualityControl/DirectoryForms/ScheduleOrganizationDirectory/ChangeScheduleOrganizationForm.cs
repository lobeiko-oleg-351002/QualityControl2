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
    public partial class ChangeScheduleOrganizationForm : ChangeForm
    {
        BllScheduleOrganization oldScheduleOrganization;
        public ChangeScheduleOrganizationForm() : base()
        {
            InitializeComponent();
        }

        public ChangeScheduleOrganizationForm(DirectoryForm parent, BllScheduleOrganization oldScheduleOrganization, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldScheduleOrganization = oldScheduleOrganization;
            textBox1.Text = oldScheduleOrganization.Name;
            textBox2.Text = oldScheduleOrganization.Address;
            textBox3.Text = oldScheduleOrganization.Description;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название организации", "Оповещение");
            }
            else
            {
                oldScheduleOrganization.Name = textBox1.Text;
                oldScheduleOrganization.Address = textBox2.Text;
                oldScheduleOrganization.Description = textBox3.Text;

                IScheduleOrganizationService Service = new ScheduleOrganizationService(uow);
                Service.Update(oldScheduleOrganization);
                base.button2_Click(sender, e);
            }
        }

    }
}
