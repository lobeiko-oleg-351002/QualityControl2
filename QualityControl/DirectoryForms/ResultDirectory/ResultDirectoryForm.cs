using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Entities;
using QualityControl_Server;

namespace QualityControl_Server.Forms.ResultDirectory
{
    public partial class ResultDirectoryForm : DirectoryForm
    {
        List<string> numberHistory = new List<string>();
        List<string> welderHistory = new List<string>();
        List<string> weldingTypeHistory = new List<string>();
        List<string> defectHistory = new List<string>();
        List<string> normHistory = new List<string>();
        List<string> qualityHistory = new List<string>();

        private void InitHistoryLists()
        {
            AppConfigManager cfg = new AppConfigManager();
            string history = "";
            history = cfg.GetTagValue(cfg.number);
            numberHistory = ParseString(history);
            history = cfg.GetTagValue(cfg.welder);
            welderHistory = ParseString(history);
            history = cfg.GetTagValue(cfg.weldingType);
            weldingTypeHistory = ParseString(history);
            history = cfg.GetTagValue(cfg.defect);
            defectHistory = ParseString(history);
            history = cfg.GetTagValue(cfg.norm);
            normHistory = ParseString(history);
            history = cfg.GetTagValue(cfg.quality);
            qualityHistory = ParseString(history);

        }

        public ResultDirectoryForm()
        {
            InitializeComponent();
            InitHistoryLists();
        }

        BllResultLib resultLib;
        
        public void RenameColumnsForVIK()
        {
            dataGridView1.Columns[0].HeaderText = "Стадия контроля";
            dataGridView1.Columns[1].HeaderText = "Исполнитель";
            dataGridView1.Columns[2].HeaderText = "Результат приёмки";
            dataGridView1.Columns[3].HeaderText = "Особые отметки";
            dataGridView1.Columns[4].HeaderText = "Примечание";
            dataGridView1.Columns[5].HeaderText = "Дата контроля";

            if (weldingTypeHistory.Count() == 0)
            {
                weldingTypeHistory.Add("Принят");
                weldingTypeHistory.Add("Не принят");
            }
            if (numberHistory.Count() == 0)
            {
                numberHistory.Add("Сборка");
                numberHistory.Add("Сварка");
                numberHistory.Add("Приёмочный");
            }
        }

        public ResultDirectoryForm(BllResultLib lib)
        {
            InitializeComponent();
            InitHistoryLists();
            resultLib = lib;
            foreach (var result in lib.Entities)
            {
                AddResultRowToDataGrid(result);
            }
        }

        private List<string> ParseString(string items)
        {
            var list = new List<string>(items.Split(','));
            for(int i = 0; i < list.Count;)
            {
                if (list[i] == "")
                {
                    list.Remove(list[i]);
                }
                else
                {
                    i++;
                }
            }
            return list;
        }


        private void AddResultRowToDataGrid(BllResult result)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);
            row.Cells[0].Value = result.Number;
            row.Cells[1].Value = result.Welder;
            row.Cells[2].Value = result.WeldingType;
            row.Cells[3].Value = result.DefectDescription;
            row.Cells[4].Value = result.Norm;
            row.Cells[5].Value = result.Quality;
            dataGridView1.Rows.Add(row);
        }

        private void SetAutocompleteCell(DataGridViewEditingControlShowingEventArgs e, List<string> history)
        {
            TextBox autoText = e.Control as TextBox;
            autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            TransformHistoryToCollection(DataCollection, history);
            autoText.AutoCompleteCustomSource = DataCollection;

        }

        protected override void button1_Click(object sender, EventArgs e)
        {
            resultLib.Entities.Add(new BllResult());
            dataGridView1.Rows.Add(new DataGridViewRow());
            base.button1_Click(sender, e);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                resultLib.Entities.RemoveAt(row.Index);
                dataGridView1.Rows.Remove(row);
            }
            base.button2_Click(sender, e);
        }

        protected override void button4_Click(object sender, EventArgs e)
        {
            var results = resultLib.Entities;
            var rows = dataGridView1.Rows;
            for (int i = 0; i < results.Count; i++)
            {
                results[i].Number = (string)rows[i].Cells[0].Value;
                RefreshHistoryList(numberHistory, results[i].Number.ToString());
                results[i].Welder = (string)rows[i].Cells[1].Value;
                RefreshHistoryList(welderHistory, results[i].Welder);
                results[i].WeldingType = (string)rows[i].Cells[2].Value;
                RefreshHistoryList(weldingTypeHistory, results[i].WeldingType);
                results[i].DefectDescription = (string)rows[i].Cells[3].Value;
                RefreshHistoryList(defectHistory, results[i].DefectDescription);
                results[i].Norm = (string)rows[i].Cells[4].Value;
                RefreshHistoryList(normHistory, results[i].Norm);
                results[i].Quality = (string)rows[i].Cells[5].Value;
                RefreshHistoryList(qualityHistory, results[i].Quality);
            }
            SaveHistoryLists();
            base.button4_Click(sender, e);
        }

        const int historyLength = 10;

        private void RefreshHistoryList(List<string> history, string value)
        {
            if (value != null)
            {
                if (!history.Any(v => v == value))
                {
                    history.Add(value);
                    var excessCount = history.Count - historyLength;
                    if (excessCount > 0)
                    {
                        history.RemoveRange(0, excessCount);
                    }
                }
            }
        }

        private void SaveHistoryLists()
        {
            AppConfigManager cfg = new AppConfigManager();
            cfg.ChangeTagValue(cfg.number, TransformListToString(numberHistory));
            cfg.ChangeTagValue(cfg.welder, TransformListToString(welderHistory));
            cfg.ChangeTagValue(cfg.weldingType, TransformListToString(weldingTypeHistory));
            cfg.ChangeTagValue(cfg.norm, TransformListToString(normHistory));
            cfg.ChangeTagValue(cfg.defect, TransformListToString(defectHistory));
            cfg.ChangeTagValue(cfg.quality, TransformListToString(qualityHistory));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataGridViewRow temp = new DataGridViewRow();
                temp.CreateCells(dataGridView1);
                for(int i = 0; i < row.Cells.Count; i++)
                {
                    temp.Cells[i].Value = row.Cells[i].Value;
                }
                dataGridView1.Rows.Add(temp);
                resultLib.Entities.Add(new BllResult());
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string title = dataGridView1.Columns[dataGridView1.SelectedCells[0].ColumnIndex].Name;
            if (title == dataGridView1.Columns[0].Name)
            {
                SetAutocompleteCell(e, numberHistory);
            }
            if (title == dataGridView1.Columns[1].Name)
            {
                SetAutocompleteCell(e, welderHistory);
            }
            if (title == dataGridView1.Columns[2].Name)
            {
                SetAutocompleteCell(e, weldingTypeHistory);
            }
            if (title == dataGridView1.Columns[3].Name)
            {
                SetAutocompleteCell(e, defectHistory);
            }
            if (title == dataGridView1.Columns[4].Name)
            {
                SetAutocompleteCell(e, normHistory);
            }
            if (title == dataGridView1.Columns[5].Name)
            {
                SetAutocompleteCell(e, qualityHistory);
            }
        }

        private void TransformHistoryToCollection(AutoCompleteStringCollection collection, List<string> history)
        {
            foreach(var item in history)
            {
                collection.Add(item);
            }
        }

        private string TransformListToString(List<string> list)
        {
            string res = "";
            foreach (var str in list)
            {
                res += str + ',';
            }
            return res;
        }
    }
}
