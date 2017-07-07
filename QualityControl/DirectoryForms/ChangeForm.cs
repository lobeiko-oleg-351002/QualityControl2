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

namespace QualityControl_Server.Forms
{
    public partial class ChangeForm : GeneralForm
    {
        protected DirectoryForm parent;
        public ChangeForm()
        {
            InitializeComponent();
        }

        public ChangeForm(DirectoryForm parent) : base()
        {
            InitializeComponent();
            this.parent = parent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
