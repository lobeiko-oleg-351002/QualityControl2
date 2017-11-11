using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QualityControl_Server
{
    class AppConfigManager
    {
        public const string connectionStringName = "ServiceDB";
        public string outputLocationTag = "OutputLocation";
        public string clearEquipmentAfterAdding = "clearEquipmentAfterAdding";
        public string clearDefectsAfterAdding = "clearDefectsAfterAdding";
        public string clearEmployeesAfterAdding = "clearEmployeesAfterAdding";
        public string copyEmployeesToAllTypesOfMethods = "copyEmployeesToAllTypesOfProtocols";
        public string userIsReviewer = "userIsReviewer";
        public string hideControlMethods = "hideControlMethods";
        public string daysBeforeDeadline = "daysBeforeDeadline";
        public string registrationDate = "registrationDate";
        public string functionalityLevel = "functionalityLevel";

        public string number = "_Number";
        public string weldingType = "_WeldingType";
        public string defect = "_Defect";
        public string norm = "_Norm";
        public string quality = "_Quality";
        public string welder = "_Welder";

        string configPath;
        ExeConfigurationFileMap fileMap;

        private string conStr;

        public AppConfigManager()
        {
            string mydoc = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            configPath = mydoc + "\\Управление качеством\\settings.config";
            conStr = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=" + mydoc + "\\Управление качеством\\Data\\ServiceDB.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
        }

        public bool ChangeConnectionString(string newValue)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(GetTagValue("connectionString"));
                //
                builder.ConnectionString = newValue;
                ChangeTagValue("connectionString", builder.ConnectionString);


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetConnectionString()
        {
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var tag = "connectionString";
            if (configuration.AppSettings.Settings[tag] == null)
            {
                CreateTag(tag, conStr);
                return conStr;
            }
            return configuration.AppSettings.Settings[tag].Value;
        }

        public string GetAttachDbFileName()
        {
            string connectionString = GetConnectionString();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            return builder.AttachDBFilename;
        }

        public string GetFilePath()
        {
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config.FilePath;
        }

        public void ChangeOutputLocation(string path)
        {
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            //config.AppSettings.Settings.Add("OutputLocation", "C:\\");
            //config.Save(ConfigurationSaveMode.Minimal);
            config.AppSettings.Settings[outputLocationTag].Value = path;
            config.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetOutputLocation()
        {
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return configuration.AppSettings.Settings["OutputLocation"]?.Value;
        }

        public string GetTagValue(string tag)
        {
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (configuration.AppSettings.Settings[tag] == null)
            {
                if (tag == daysBeforeDeadline)
                {
                    CreateTag(tag, "365");
                    return "365";
                }
                else
                if ( tag == outputLocationTag)
                {
                    CreateTag(tag, "C:\\");
                    return "C:\\";
                }
                else
                if (tag[0] == '_')
                {
                    CreateHistoryTag(tag);
                    return "";
                }
                else
                {
                    CreateTag(tag, "false");
                    return "false";
                }
            }
            return configuration.AppSettings.Settings[tag].Value;
        }

        public void ChangeTagValue(string tag, string value)
        {
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings[tag].Value = value;
            config.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void CreateTag(string tag, string value)
        {
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(tag, value);
            config.Save(ConfigurationSaveMode.Minimal);
        }

        public void CreateHistoryTag(string tag)
        {
            CreateTag(tag, "");
        }

        public void SetTagValue(string tag, string value)
        {
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (configuration.AppSettings.Settings[tag] == null)
            {
                CreateTag(tag, value);
            }
            else
            {
                ChangeTagValue(tag, value);
            }
        }


    }
}
