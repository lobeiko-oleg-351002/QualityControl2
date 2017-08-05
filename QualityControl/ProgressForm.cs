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

namespace QualityControl_Server
{
    public partial class ProgressForm : GeneralForm
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

  
        public void IncNumber()
        {
            progressBar1.PerformStep();
            label1.Text = String.Format(text + ": {0} из {1}.", progressBar1.Value, progressBar1.Maximum);
            
        }

        string text = "";

        public void SetWholeAmount(int count)
        {
            progressBar1.Maximum = count;
            progressBar1.Value = 0;
        }
        public ProgressForm(string text)
        {
            InitializeComponent();
            CenterToScreen();

            this.text = text;
        }
    }
}
