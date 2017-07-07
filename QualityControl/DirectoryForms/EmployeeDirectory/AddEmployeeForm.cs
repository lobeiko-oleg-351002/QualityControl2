using QualityControl_Server.Forms.SertificateDirectory;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Entities;
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Server.Forms.EmployeeDirectory
{
    public partial class AddEmployeeForm : AddForm
    {

        public AddEmployeeForm() : base()
        {
            InitializeComponent();
        }

        public AddEmployeeForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {

            BllEmployee Employee = new BllEmployee
            {
                Name = textBox1.Text,
                Sirname = textBox2.Text,
                Fathername = textBox3.Text,
                Function = textBox4.Text,
                MedicalCheckDate = dateTimePicker1.Value,
                KnowledgeCheckDate = dateTimePicker2.Value
            };
            IEmployeeService Service = new EmployeeService(uow);

            string errorMessage = "Неверно указаны данные";
            bool isError = false;
            if (Employee.Name == "" || Employee.Sirname == "" || Employee.Fathername == "" || Employee.Function == "")
            {
                isError = true;
            }

            if (isError == false)
            {
                Service.Create(Employee);
                base.button2_Click(sender, e);
            }
            else
            {
                MessageBox.Show(errorMessage, "Оповещение");
            }                      
        }

    }
}
