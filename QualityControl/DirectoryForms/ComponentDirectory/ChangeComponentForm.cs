using QualityControl_Server.Forms.TemplateDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Services;
using BLL.Services.Interface;
using BLL.Entities;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms.IndustrialObjectDirectory;

namespace QualityControl_Server.Forms.ComponentDirectory
{
    public partial class ChangeComponentForm : ChangeForm
    {
        BllComponent oldComponent;

        public ChangeComponentForm() : base()
        {
            InitializeComponent();
        }
        public ChangeComponentForm(DirectoryForm parent, BllComponent oldComponent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldComponent = oldComponent;
            textBox1.Text = oldComponent.Name;
            textBox2.Text = oldComponent.Pressmark;
            maskedTextBox1.Text = oldComponent.Template != null ? oldComponent.Template.Name : "<отсутствует>";
            maskedTextBox2.Text = oldComponent.IndustrialObject != null ? oldComponent.IndustrialObject.Name : "<не указан>";
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldComponent.Name = textBox1.Text;
                oldComponent.Pressmark = textBox2.Text;
                IComponentService Service = new ComponentService(uow);
                Service.Update(oldComponent);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseTemplateForm templateForm = new ChooseTemplateForm(uow);
            templateForm.ShowDialog(this);
            oldComponent.Template = templateForm.GetChosenTemplate();
            if (oldComponent.Template != null)
            {
                maskedTextBox1.Text = oldComponent.Template.Name;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChooseIndustrialObjectForm templateForm = new ChooseIndustrialObjectForm(uow);
            templateForm.ShowDialog(this);
            oldComponent.IndustrialObject = templateForm.GetChosenIndustrialObject();
            if (oldComponent.IndustrialObject!= null)
            {
                maskedTextBox2.Text = oldComponent.IndustrialObject.Name;
            }
        }
    }
}
