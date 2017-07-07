
using DAL.Repositories.Interface;
using QualityControl_Server.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QualityControl_Server.Forms
{
    public partial class AddForm : GeneralForm
    {
        protected DirectoryForm parent;

        public AddForm() : base()
        {
            InitializeComponent();
        }

        public AddForm(DirectoryForm parent) : base()
        {
            InitializeComponent();
            CenterToParent();
            this.parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        virtual protected void button2_Click(object sender, EventArgs e)
        {
            parent.RefreshData();
            Close();
        }
    }
}
