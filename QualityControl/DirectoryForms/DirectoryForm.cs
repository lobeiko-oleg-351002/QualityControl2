using BLL.Entities.Interface;
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
    public partial class DirectoryForm : GeneralForm
    {
        public DirectoryForm() : base()
        {           
            InitializeComponent();
            CenterToScreen();
        }

        protected virtual void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected virtual void button1_Click(object sender, EventArgs e)
        {
            
        }

        public virtual void RefreshData() {
        }

        protected virtual void button2_Click(object sender, EventArgs e)
        {

        }

        protected virtual void button3_Click(object sender, EventArgs e)
        {

        }

        public virtual void SelectRow(IBllEntity entity)
        {

        }

        protected void StartProgressBar(int max, int step)
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = max;
            progressBar1.Step = step;
            progressBar1.Value = 0;
        }

        protected void StopProgressBar()
        {
            progressBar1.Visible = false;
        }

        protected void ProgressBarStep()
        {
            progressBar1.PerformStep();
        }


    }
}
