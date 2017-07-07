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
        //BllComponentLib ComponentLib;
        List<BllSelectedEntity<BllComponent>> Components = new List<BllSelectedEntity<BllComponent>>();
        public ChangeIndustrialObjectForm(DirectoryForm parent, BllIndustrialObject oldIndustrialObject, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.oldIndustrialObject = oldIndustrialObject;
            this.uow = uow;
            //ComponentLib = oldIndustrialObject.ComponentLib;
            textBox1.Text = oldIndustrialObject.Name;
            //if (oldIndustrialObject.ComponentLib != null)
            //{
            //    foreach (var Component in oldIndustrialObject.ComponentLib.SelectedEntities)
            //    {
            //        comboBox1.Items.Add(Component.Entity.Name);
            //        Components.Add(Component);
            //    }
            //}
            //comboBox1.SelectedIndex = 0;
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
               // oldIndustrialObject.ComponentLib.SelectedEntities = Components;

                IIndustrialObjectService Service = new IndustrialObjectService(uow);
                Service.Update(oldIndustrialObject);
                base.button2_Click(sender, e);
            }
        }

        BllComponent Component;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm(uow);
            ComponentForm.ShowDialog(this);
            BllComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Components.Add(new BllSelectedEntity<BllComponent>
                {
                    Entity = Component
                });
                comboBox1.Items.Add(Component.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Components.RemoveAt(comboBox1.SelectedIndex);
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
        }
    }
}
