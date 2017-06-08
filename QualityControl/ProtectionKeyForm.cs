﻿using QualityControl_Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client
{
    public partial class ProtectionKeyForm : Form
    {
        public bool isActivated { get; private set; }
        public ProtectionKeyForm()
        {
            InitializeComponent();
            string randomString = ProtectionManager.RandomString(14);
            textBox1.Text = randomString;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            AppConfigManager config = new AppConfigManager();
            isValid = ProtectionManager.CompareHash(textBox1.Text, textBox2.Text);
            if (isValid)
            {
                MessageBox.Show("Программа активирована успешно", "Оповещение");
                config.SetTagValue(config.functionalityLevel, "базовая");
                config.SetTagValue(config.registrationDate, DateTime.Now.ToString("dd.MM.yyyy"));
                isActivated = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ключ введён неверно", "Оповещение");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppConfigManager config = new AppConfigManager();
            config.SetTagValue(config.functionalityLevel, "органиченная");
            config.SetTagValue(config.registrationDate, "-");
            Close();
        }

    }
}
