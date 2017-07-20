using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Server.Forms.TemplateDirectory;
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
using QualityControl_Server.Forms.IndustrialObjectDirectory;

namespace QualityControl_Server.Forms.ComponentDirectory
{
    public partial class AddComponentForm : AddForm
    {
        public AddComponentForm() : base()
        {
            InitializeComponent();
        }

        public AddComponentForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        BllTemplate template;
        BllIndustrialObject industrialObject;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseTemplateForm templateForm = new ChooseTemplateForm(uow);
            templateForm.ShowDialog(this);
            template = templateForm.GetChosenTemplate();
            if (template != null)
            {
                maskedTextBox1.Text = template.Name;
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllComponent component = new BllComponent
                {
                    Name = textBox1.Text,
                    Template = template,
                    Pressmark = textBox2.Text,
                    IndustrialObject = industrialObject
                };
                IComponentService Service = new ComponentService(uow);
                Service.Create(component);
                base.button2_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChooseIndustrialObjectForm templateForm = new ChooseIndustrialObjectForm(uow);
            templateForm.ShowDialog(this);
            industrialObject = templateForm.GetChosenIndustrialObject();
            if (industrialObject != null)
            {
                maskedTextBox2.Text = industrialObject.Name;
            }
        }
    }
}
