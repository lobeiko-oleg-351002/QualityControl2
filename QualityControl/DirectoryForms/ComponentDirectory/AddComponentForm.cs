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
using System.Data.OleDb;

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
            if (filename == "")
            {
                BllComponent component = new BllComponent
                {
                    Name = textBox1.Text,
                    Template = template,
                    Pressmark = textBox2.Text,
                    IndustrialObject = industrialObject,
                    Description = textBox5.Text,
                    Count = (int)numericUpDown1.Value
                };
                IComponentService Service = new ComponentService(uow);
                Service.Create(component);
            }
            else
            {
                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + "Sheet1" + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                IComponentService Service = new ComponentService(uow);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var name = data.Rows[i]["Описание"].ToString();
                    var count = data.Rows[i]["Кол-во"].ToString();

                    if (name != "")
                    {
                        BllComponent entity = new BllComponent
                        {
                            Name = name,
                            Pressmark = data.Rows[i]["Марка элемента"].ToString(),
                            IndustrialObject = industrialObject,
                            Template = template,
                            Count = count != "" ? int.Parse(count) : 1,
                            Description = data.Rows[i]["Размер"].ToString() +  "/" + data.Rows[i]["Масса"].ToString()
                        };
                        Service.Create(entity);
                    }
                }
            }
            base.button2_Click(sender, e);           
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

        String filename = "";
        private void button5_Click(object sender, EventArgs e)
        {
            String name = "Sheet1";
            
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                filename = openFileDialog1.FileName;
            }
            textBox3.Text = filename;
        }
    }
    
}
