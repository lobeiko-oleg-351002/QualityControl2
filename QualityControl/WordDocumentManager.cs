﻿using Microsoft.Office.Interop.Word;
using QualityControl_Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Server
{
    public class WordDocumentManager
    {
        IUnitOfWork uow;
        public WordDocumentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private void FillVIKReport(Application wordApp, List<BllJournal> journals, BllUser user)
        {
            //Find and replace:
            this.FindAndReplace(wordApp, "ProtocolNumber", journals[0].ControlMethodsLib.Entities[0].ProtocolNumber.ToString());
            this.FindAndReplace(wordApp, "ControlDate_Day", journals[0].ControlDate.Value.Day.ToString());
            string month = "";
            switch(journals[0].ControlDate.Value.Month.ToString())
            {
                case "1":
                    month = "января";
                    break;
                case "2":
                    month = "февраля";
                    break;
                case "3":
                    month = "марта";
                    break;
                case "4":
                    month = "апреля";
                    break;
                case "5":
                    month = "мая";
                    break;
                case "6":
                    month = "июня";
                    break;
                case "7":
                    month = "июля";
                    break;
                case "8":
                    month = "августа";
                    break;
                case "9":
                    month = "сентября";
                    break;
                case "10":
                    month = "октября";
                    break;
                case "11":
                    month = "ноября";
                    break;
                case "12":
                    month = "декабря";
                    break;
            }

            this.FindAndReplace(wordApp, "ControlDate_Month", month);
            this.FindAndReplace(wordApp, "ControlDate_Year", journals[0].ControlDate.Value.Year.ToString());
            this.FindAndReplace(wordApp, "RequestNum", journals[0].RequestNumber.Value.ToString());
            this.FindAndReplace(wordApp, "RequestDate", "");
            this.FindAndReplace(wordApp, "Customer", journals[0].Customer != null ? journals[0].Customer.Organization : "<не указан>");
            this.FindAndReplace(wordApp, "IndustrialObject", journals[0].IndustrialObject != null ? journals[0].IndustrialObject.Name : "<не указан>");
            this.FindAndReplace(wordApp, "Component", journals[0].Component != null ? journals[0].Component.Name : "<не указан>");

            string code = "";
            foreach(var doc in journals[0].ControlMethodsLib.Entities[0].RequirementDocumentationLib.SelectedEntities)
            {
                code += doc.Entity.Pressmark + "; ";
            }
            this.FindAndReplace(wordApp, "RequirementDocCode", code);

            code = "";
            foreach (var doc in journals[0].ControlMethodsLib.Entities[0].ControlMethodDocumentationLib.SelectedEntities)
            {
                code += doc.Entity.Pressmark + "; ";
            }
            this.FindAndReplace(wordApp, "ControlMethodDocCode", code);

            AppConfigManager configManager = new AppConfigManager();
            if (bool.Parse(configManager.GetTagValue(configManager.userIsReviewer)))
            {
                this.FindAndReplace(wordApp, "Employee", user.Employee != null ?
                    user.Employee.Sirname + " " +
                    user.Employee.Name[0] + ". " +
                    user.Employee.Fathername[0] + "." : "<не указано>");
            }
            else
            {
                this.FindAndReplace(wordApp, "Employee", journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities.Count != 0 ?
                    journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities[0].Entity.Sirname + " " +
                    journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities[0].Entity.Name[0] + ". " +
                    journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities[0].Entity.Fathername[0] + "." : "<не указано>");
            }

            this.FindAndReplace(wordApp, "Light", journals[0].ControlMethodsLib.Entities[0].Light.Value.ToString());

            BllCertificate certificate = null;
            if (journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities.Count != 0)
            {
                ICertificateService service = new CertificateService(uow);
                certificate =  service.GetCertificateByEmployeeAndControlName(
                    journals[0].ControlMethodsLib.Entities[0].EmployeeLib.SelectedEntities[0].Entity, journals[0].ControlMethodsLib.Entities[0].ControlName);
            }
            this.FindAndReplace(wordApp, "Certificate", certificate != null ? certificate.Title : "-");
            this.FindAndReplace(wordApp, "CertificateDate", certificate != null ? certificate.CheckDate.ToString("dd.MM.yyyy") : "-");

        }


        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace, ref matchKashida,
                        ref matchDiactitics, ref matchAlefHamza,
                        ref matchControl);
        }




        public void CreateWordDocument(object savaAs, List<BllJournal> journals, BllUser user)
        {
            List<int> processesbeforegen = getRunningProcesses();
            object missing = Missing.Value;

            string vikPath = "\\Template\\vik.docx";
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            object docLocation = executableLocation + vikPath;

            Application wordApp = new Application();

            Document aDoc = null;

            if (File.Exists((string)docLocation))
            {
                DateTime today = DateTime.Now;

                object readOnly = false; //default
                object isVisible = false;

                wordApp.Visible = false;

                try
                {
                    aDoc = wordApp.Documents.Open(ref docLocation, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing, ref missing);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                aDoc.Activate();

                FillVIKReport(wordApp, journals, user);

                //insert the picture:
                //Image img = resizeImage(pathImage, new Size(200, 90));
                //tempPath = System.Windows.Forms.Application.StartupPath + "\\Images\\~Temp\\temp.jpg";
                //img.Save(tempPath);

                Object oMissed = aDoc.Paragraphs[1].Range; //the position you want to insert
                Object oLinkToFile = false;  //default
                Object oSaveWithDocument = true;//default
                                                // aDoc.InlineShapes.AddPicture(tempPath, ref oLinkToFile, ref oSaveWithDocument, ref oMissed);

                Tables tables = aDoc.Tables;

                foreach (Table t in tables)
                {
                    if (t.Columns.Count == 0) { }
                }
                if (tables.Count > 4)
                {
                    //Get the first table in the document
                    Table tableEquipment = tables[2];
                    Table tableResult = tables[4];

                    List<BllEquipment> usedEq = new List<BllEquipment>();
                    int rowsCount = tableEquipment.Rows.Count;
                    int coulmnsCount = tableEquipment.Columns.Count;

                    for (int i = 0; i < journals.Count; i++)
                    {
                        BllControl vik = null;
                        foreach(var control in journals[i].ControlMethodsLib.Entities)
                        {
                            if (control.ControlName.Name == "ВИК")
                            {
                                vik = control;
                                break;
                            }
                        }
                        if (vik != null)
                        {
                            foreach(var eq in vik.EquipmentLib.SelectedEntities)
                            {
                                bool isUsed = false;
                                foreach(var item in usedEq)
                                {
                                    if (eq.Entity.Id == item.Id)
                                    {
                                        isUsed = true;
                                        break;
                                    }
                                }

                                if (!isUsed)
                                {
                                    Row row = tableEquipment.Rows.Add(ref missing);

                                    row.Cells[1].Range.Text = eq.Entity.Name + ", " + eq.Entity.Type;
                                    row.Cells[2].Range.Text = eq.Entity.FactoryNumber.ToString();
                                    row.Cells[3].Range.Text = eq.Entity.NumberOfTechnicalCheck + ", " + eq.Entity.NextTechnicalCheckDate.Value.ToString("dd.MM.yyyy");
                                    usedEq.Add(eq.Entity);
                                }
                            }
                            foreach (var res in vik.ResultLib.Entities)
                            {
                                Row row = tableResult.Rows.Add(ref missing);

                                row.Cells[1].Range.Text = (tableResult.Rows.Count - 2).ToString();
                                row.Cells[2].Range.Text = journals[i].Weight + ", " + (journals[i].Material != null ? journals[i].Material.Name : "<не указано>");
                                row.Cells[3].Range.Text = res.Welder;
                                row.Cells[4].Range.Text = res.Number.ToString();
                                row.Cells[5].Range.Text = res.DefectDescription;
                                row.Cells[6].Range.Text = res.Quality;
                                // row.Cells[j].WordWrap = true;
                                // row.Cells[j].Range.Underline = WdUnderline.wdUnderlineNone;
                                // row.Cells[j].Range.Bold = 0;
                            }
                        }
                        
                    }
        }

        #region Print Document :
        /*object copies = "1";
        object pages = "1";
        object range = Word.WdPrintOutRange.wdPrintCurrentPage;
        object items = Word.WdPrintOutItem.wdPrintDocumentContent;
        object pageType = Word.WdPrintOutPages.wdPrintAllPages;
        object oTrue = true;
        object oFalse = false;

        Word.Document document = aDoc;
        object nullobj = Missing.Value;
        int dialogResult = wordApp.Dialogs[Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint].Show(ref nullobj);
        wordApp.Visible = false;
        if (dialogResult == 1)
        {
            document.PrintOut(
            ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
            ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
            ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);
        }
        */
        #endregion

    }
            else
            {
                //MessageBox.Show("file dose not exist.");
                return;
            }

            //Save as: filename
            aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

            //Close Document:
            aDoc.Close(ref missing, ref missing, ref missing);
           // File.Delete(tempPath);
           // MessageBox.Show("File created.");
            List<int> processesaftergen = getRunningProcesses();
            killProcesses(processesbeforegen, processesaftergen);
        }

        private void CloseDoc()
        {

        }


        public List<int> getRunningProcesses()
        {
            List<int> ProcessIDs = new List<int>();
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                    continue;
                if (clsProcess.ProcessName.Contains("WINWORD"))
                {
                    ProcessIDs.Add(clsProcess.Id);
                }
            }
            return ProcessIDs;
        }


        private void killProcesses(List<int> processesbeforegen, List<int> processesaftergen)
        {
            foreach (int pidafter in processesaftergen)
            {
                bool processfound = false;
                foreach (int pidbefore in processesbeforegen)
                {
                    if (pidafter == pidbefore)
                    {
                        processfound = true;
                    }
                }

                if (processfound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidafter);
                    clsProcess.Kill();
                }
            }
        }
    }
}
