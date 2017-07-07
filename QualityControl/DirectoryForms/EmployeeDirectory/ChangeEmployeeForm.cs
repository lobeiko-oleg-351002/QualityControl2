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
    public partial class ChangeEmployeeForm : ChangeForm
    {
        BllEmployee oldEmployee;
        public ChangeEmployeeForm() : base()
        {
            InitializeComponent();
        }


        public ChangeEmployeeForm(DirectoryForm parent, BllEmployee oldEmployee, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldEmployee = oldEmployee;

            textBox1.Text = oldEmployee.Name;
            textBox2.Text = oldEmployee.Fathername;
            textBox3.Text = oldEmployee.Sirname;
            textBox4.Text = oldEmployee.Function;

            dateTimePicker1.Value = oldEmployee.MedicalCheckDate.Value;
            dateTimePicker2.Value = oldEmployee.KnowledgeCheckDate.Value;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            oldEmployee.Name = textBox1.Text;
            oldEmployee.Sirname = textBox2.Text;
            oldEmployee.Fathername = textBox3.Text;
            oldEmployee.Function = textBox4.Text;
            oldEmployee.MedicalCheckDate = dateTimePicker1.Value;
            oldEmployee.KnowledgeCheckDate = dateTimePicker2.Value;

            IEmployeeService Service = new EmployeeService(uow);
            string errorMessage = "Неверно указаны данные";
            bool isError = false;
            if (oldEmployee.Name == "" || oldEmployee.Sirname == "" || oldEmployee.Fathername == "" || oldEmployee.Function == "")
            {
                isError = true;
            }

            if (isError == false)
            {
                Service.Update(oldEmployee);
                base.button2_Click(sender, e);
            }
            else
            {
                MessageBox.Show(errorMessage, "Оповещение");
            }           
        }

        
    }
}
