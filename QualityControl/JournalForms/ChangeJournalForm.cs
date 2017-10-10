using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server
{
    public partial class ChangeJournalForm : JournalForm
    {
        public ChangeJournalForm()
        {
            InitializeComponent();
        }

        public ChangeJournalForm(BllJournal oldJournal, BllUser user, IUnitOfWork uow) : base(oldJournal, user, uow)
        {
            InitializeComponent();
        }

        protected override void SetComponent(BllComponent entity)
        {
            Journal.Component = entity;
            comboBox3.Text = entity.Name + " " + entity.Pressmark;
            if (entity.IndustrialObject != null)
            {
                SetIndustrialObject(entity.IndustrialObject);
            }
            if (entity.Count != null)
            {

                label1.Visible = true;
                label9.Visible = true;
                numericUpDown1.Value = Journal.Amount.Value;
                int controlledCount = uow.Journals.GetControlledCount(entity.Id);
                int count = entity.Count.Value - controlledCount + Journal.Amount.Value;
                if (count >= 0)
                {
                    label9.Text = count.ToString();
                }
                else
                {
                    label9.Text = entity.Count.Value.ToString() + " - " + (controlledCount - Journal.Amount.Value).ToString() + " (пересорт)";
                }
            }
            if (entity.Description != null)
            {
                textBox3.Text = entity.Description;
            }
        }
    }
}
