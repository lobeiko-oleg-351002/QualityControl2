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
    public partial class ServiceInfo : Form
    {
        public ServiceInfo()
        {
            InitializeComponent();
        }

        private void ServiceInfo_Load(object sender, EventArgs e)
        {
            AppConfigManager config = new AppConfigManager();
            label2.Text = config.GetTagValue(config.registrationDate);
            label3.Text = config.GetTagValue(config.functionalityLevel);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}
