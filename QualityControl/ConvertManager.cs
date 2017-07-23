using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Security.AccessControl;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using BLL.Entities;

namespace QualityControl_Server
{
    public class ConvertManager
    {

        private static string ConvertCellDataToString(DataGridViewCell cell)
        {
            string cellString = "";
            if (cell as DataGridViewComboBoxCell != null)
            {                
                foreach (string item in ((DataGridViewComboBoxCell)cell).Items)
                {
                    cellString += item + "\n";
                }
                cellString.Remove(cellString.Length - 1);
            }
            if (cell as DataGridViewTextBoxCell != null)
            {                
                try
                {
                    cellString = cell.Value.ToString();
                    DateTime myDate = DateTime.Parse(cellString);
                    cellString = ConvertDateTimeToString(myDate);
                }
                catch { }
            }
            return cellString;
        }

        private static string ConvertDateTimeToString(DateTime date)
        {
            string res = date.ToString("dd.MM.yyyy");
            return res;
        }
            
        public static void ConvertDataGridToPdf(DataGridView dataGridView, string folderPath)
        {
            string fontsfolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);
            BaseFont baseFont = BaseFont.CreateFont(fontsfolder + "\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView.ColumnCount);
            pdfTable.DefaultCell.Padding = 4;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;


            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,font));
                cell.BackgroundColor = new Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Visible)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell as DataGridViewImageCell != null)
                        {
                            pdfTable.AddCell((Image)((DataGridViewImageCell)cell).Value);
                        }
                        else
                        {
                            string cellString = ConvertCellDataToString(cell);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(cellString, font));
                            pdfTable.AddCell(pdfCell);
                        }
                    }
                }
            }

            //Exporting to PDF
            ExportPdfTable(pdfTable, folderPath);
        }

        public static void ConvertControlResultToPdf(BllControl control, BllJournal journal, string folderPath)
        {
            string fontsfolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);
            BaseFont baseFont = BaseFont.CreateFont(fontsfolder + "\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            List<BllResult> results = control.ResultLib.Entities;
            //Creating iTextSharp Table from the DataTable data
            const int amountOfColumns = 9;
            PdfPTable pdfTable = new PdfPTable(amountOfColumns);

            pdfTable = InitPdfTable(pdfTable, font);

            //Adding DataRow
            foreach (BllResult result in results)
            {
                pdfTable = AddResultToPdfTable(pdfTable, result, journal, font);
            }

            //Exporting to PDF
            ExportPdfTable(pdfTable, folderPath);
        }

        private static PdfPCell CreateHeaderCell(String header, iTextSharp.text.Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(header, font));
            cell.BackgroundColor = new Color(240, 240, 240);
            return cell;
        }

        public static void ConvertControlResultToExcel(BllControl control, BllJournal journal)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //ExcelApp = InitResultExcelTable(ExcelApp);

            List<BllResult> results = control.ResultLib.Entities;

            for (var i = 0; i < results.Count; i++)
            {
               // ExcelApp = AddRowToExcelTable(ExcelApp, results[i], journal, i);
            }

            ExportExcelTable(ExcelApp, control.ControlName.Name);
        }

        private static void ExportExcelTable(Microsoft.Office.Interop.Excel.Application ExcelApp, string fileName)
        {
            ExcelApp.ActiveWorkbook.SaveCopyAs(fileName);
            ExcelApp.ActiveWorkbook.Saved = true;
            ExcelApp.Quit();
        }

        private static Microsoft.Office.Interop.Excel.Workbook InitResultExcelTable(Workbook workbook, Microsoft.Office.Interop.Excel.Application ExcelApp, string controlName)
        {

            var worksheet = workbook.Worksheets.Add();
            worksheet.Name = controlName;
            ExcelApp.Columns.ColumnWidth = 20;
            ExcelApp.Cells[1, 1] = "№ п/п";
            ExcelApp.Cells[1, 2] = "Объект";
            ExcelApp.Cells[1, 3] = "Код объекта";
            ExcelApp.Cells[1, 4] = "Исполнитель";
            ExcelApp.Cells[1, 5] = "Дата контроля";
            ExcelApp.Cells[1, 6] = "Номер соединения";
            ExcelApp.Cells[1, 7] = "Дефект";
            ExcelApp.Cells[1, 8] = "Оценка качества";
            ExcelApp.Cells[1, 9] = "Тип соединения";
            ExcelApp.Cells[1, 10] = "Контроль провёл, ФИО, подпись";
            

            return workbook;
        }

        private static Microsoft.Office.Interop.Excel.Worksheet AddRowToExcelTable(Microsoft.Office.Interop.Excel.Worksheet ExcelApp, BllResult result, BllEmployee employee, BllJournal journal, int journalNum, int row, bool isControlled)
        {
            ExcelApp.Cells[row, 1] = journalNum;
            ExcelApp.Cells[row, 2] = journal.Component != null ? journal.Component.Name : "<не указано>";
            ExcelApp.Cells[row, 3] = journal.Component != null ? journal.Component.Pressmark : "<не указано>";
            ExcelApp.Cells[row, 4] = result.Welder;
            ExcelApp.Cells[row, 5] = journal.ControlDate != null ? ConvertDateTimeToString((DateTime)journal.ControlDate) : "<не указано>";
            //ExcelApp.Cells[row , 4] = journal.RequestNumber.ToString();
            ExcelApp.Cells[row, 6] = result.Number;
            ExcelApp.Cells[row, 7] = result.DefectDescription;
            ExcelApp.Cells[row, 8] = result.Quality;
            ExcelApp.Cells[row, 9] = result.WeldingType;
            ExcelApp.Cells[row, 10] = employee != null ? employee.Sirname : "";


            return ExcelApp;
        }

        public static void ConvertDataGridToExcel(DataGridView dataGridView, string folderPath)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            for (var i = 1; i <= dataGridView.Columns.Count; i++)
            {
                ExcelApp.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
            }

            for (var i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Visible)
                {
                    for (var j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = ConvertCellDataToString(dataGridView.Rows[i].Cells[j]);
                    }
                }
            }

            ExportExcelTable(ExcelApp, folderPath);
        }

        public static void ConvertChosenControlResultsToExcel(BllControlName controlName, List<BllJournal> journals, string folderPath)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //ExcelApp = InitResultExcelTable(ExcelApp);
            int rowNumber = -1;
            foreach (var journal in journals)
            {
                foreach (var control in journal.ControlMethodsLib.Entities)
                {
                    if (control.ControlName.Id == controlName.Id)
                    {
                        foreach (var result in control.ResultLib.Entities)
                        {
                            rowNumber++;
                           // ExcelApp = AddRowToExcelTable(ExcelApp, result, journal, rowNumber);
                        }

                    }
                }
            }

            ExportExcelTable(ExcelApp, folderPath);
        }

        public static void ConvertChosenControlResultsToPdf(BllControlName controlName, List<BllJournal> journals, string folderPath)
        {
            string fontsfolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts);
            BaseFont baseFont = BaseFont.CreateFont(fontsfolder + "\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);


            //Creating iTextSharp Table from the DataTable data
            const int amountOfColumns = 9;
            PdfPTable pdfTable = new PdfPTable(amountOfColumns);
            pdfTable = InitPdfTable(pdfTable, font);

            //Adding DataRow
            foreach (var journal in journals)
            {
                foreach (var control in journal.ControlMethodsLib.Entities)
                {
                    if (control.ControlName.Id == controlName.Id)
                    {
                        foreach(var result in control.ResultLib.Entities)
                        {
                            pdfTable = AddResultToPdfTable(pdfTable, result, journal, font);
                        }
                        
                    }
                }
            }

            //Exporting to PDF
            ExportPdfTable(pdfTable, folderPath);
        }

        private static PdfPTable InitPdfTable(PdfPTable pdfTable, iTextSharp.text.Font font)
        {
            float[] widths = new float[] { 20f, 50f, 60f, 50f, 40f, 60f, 60f, 50f, 40f };
            pdfTable.SetWidths(widths);

            pdfTable.DefaultCell.Padding = 2;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            pdfTable.AddCell(CreateHeaderCell("№ п/п", font));
            pdfTable.AddCell(CreateHeaderCell("Дата", font));
            pdfTable.AddCell(CreateHeaderCell("Контролёр", font));
            pdfTable.AddCell(CreateHeaderCell("Код изделия", font));
            pdfTable.AddCell(CreateHeaderCell("Номер заявки", font));
            pdfTable.AddCell(CreateHeaderCell("Тип сварного соединения", font));
            pdfTable.AddCell(CreateHeaderCell("Обнаруженные дефекты", font));
            pdfTable.AddCell(CreateHeaderCell("Оценка качества", font));
            pdfTable.AddCell(CreateHeaderCell("Подпись", font));

            return pdfTable;
        }

        private static PdfPTable AddResultToPdfTable(PdfPTable pdfTable, BllResult result, BllJournal journal, iTextSharp.text.Font font)
        {
            pdfTable.AddCell(CreateHeaderCell(result.Number.ToString(), font));
            pdfTable.AddCell(CreateHeaderCell(ConvertDateTimeToString((DateTime)journal.ControlDate), font));
            pdfTable.AddCell(CreateHeaderCell(result.Welder, font));
            pdfTable.AddCell(CreateHeaderCell(journal.Component != null ? journal.Component.Pressmark : "<не указано>", font));
            pdfTable.AddCell(CreateHeaderCell(journal.RequestNumber.ToString(), font));
            pdfTable.AddCell(CreateHeaderCell(result.WeldingType, font));
            pdfTable.AddCell(CreateHeaderCell(result.DefectDescription, font));
            pdfTable.AddCell(CreateHeaderCell(result.Quality, font));
            pdfTable.AddCell(CreateHeaderCell("   ", font));

            return pdfTable;
        }

        private static void ExportPdfTable(PdfPTable pdfTable, string folderPath)
        {
            string path = Path.GetDirectoryName(folderPath) + "\\";
            string fileName = Path.GetFileName(folderPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }

        public static void WriteJournalToExcel(List<BllJournal> journals, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbook = ExcelApp.Application.Workbooks.Add(Type.Missing);
            int journalNum = 0;
            int rowNumber = 0;
            workbook = InitResultExcelTable(workbook, ExcelApp, "ПВК");
            workbook = InitResultExcelTable(workbook, ExcelApp, "РГК");
            workbook = InitResultExcelTable(workbook, ExcelApp, "УЗК");
            workbook = InitResultExcelTable(workbook, ExcelApp, "ВИК");
            List<int> objectNumbers = new List<int>();
            objectNumbers.Add(0);
            objectNumbers.Add(0);
            foreach (var journal in journals)
            {
                int sheetNum = 0;
                //journalNum++;
                foreach (var control in journal.ControlMethodsLib.Entities)
                {
                    sheetNum++;
                    objectNumbers[sheetNum - 1]++;
                    foreach (Worksheet currentSheet in workbook.Sheets)
                    {
                        if (currentSheet.Name == control.ControlName.Name)
                        {
                            Microsoft.Office.Interop.Excel.Range last = currentSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                            rowNumber = last.Row + 1;

                            if (control.ResultLib.Entities.Count == 0)
                            {
                                //currentSheet.Cells[rowNumber , 1] = control.ProtocolNumber.ToString();
                                objectNumbers[sheetNum - 1]--;
                            }
                            foreach (var result in control.ResultLib.Entities)
                            {
                                BllEmployee chief = null;
                                if (control.EmployeeLib.SelectedEntities.Count() > 0)
                                {
                                    chief = control.EmployeeLib.SelectedEntities[0].Entity;
                                }
                                AddRowToExcelTable(currentSheet, result, chief, journal, objectNumbers[sheetNum - 1], rowNumber, control.IsControlled.Value);
                                rowNumber++;
                            }
                        }
                    }                                  
                }
            }
            workbook.Sheets[workbook.Sheets.Count].Delete();
            ExportExcelTable(ExcelApp, fileName);
        }
    }
}
