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
        BllComponentLib ComponentLib;
        List<BllComponent> Components = new List<BllComponent>();
        public AddIndustrialObjectForm() : base()
        {
            InitializeComponent();
        }

        public AddIndustrialObjectForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            ComponentLib = new BllComponentLib();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                List<BllSelectedEntity<BllComponent>> selectedComponents = new List<BllSelectedEntity<BllComponent>>();
                foreach (var element in Components)
                {
                    selectedComponents.Add(new BllSelectedEntity<BllComponent>
                    {
                        Entity = element
                    });
                }
                ComponentLib.SelectedEntities = selectedComponents;
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

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm(uow);
            ComponentForm.ShowDialog(this);
            BllComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Components.Add(Component);
                comboBox1.Items.Add(Component.Name);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Components.RemoveAt(comboBox1.SelectedIndex);
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}
