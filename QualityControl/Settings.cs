using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;
using QualityControl;
using System.IO;
using ORM;
using DAL.Repositories.Interface;
using DAL.Repositories;
using DAL.Entities;
using DAL.Entities.Interface;
using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;

namespace QualityControl_Server
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        MainForm parent;
        IUnitOfWork uow;
        AppConfigManager appConfigManager;
        string integrateConnectionString = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=<path>;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        public Settings(MainForm parent, IUnitOfWork uow)
        {
            InitializeComponent();
            this.parent = parent; 
            this.uow = uow;
            appConfigManager = new AppConfigManager();
            CenterToScreen();
        }

        private string connectionString = "";

        private void Settings_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "MDF files (*.mdf)|*.mdf";
            connectionString = appConfigManager.GetConnectionString();
            textBox1.Text = appConfigManager.GetAttachDbFileName();
            textBox2.Text = appConfigManager.GetTagValue(appConfigManager.outputLocationTag);
            checkBox1.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearEquipmentAfterAdding));
            checkBox2.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearDefectsAfterAdding));
            checkBox3.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearEmployeesAfterAdding));
            checkBox4.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.copyEmployeesToAllTypesOfMethods));
            checkBox5.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.userIsReviewer));
            checkBox6.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.hideControlMethods));
            numericUpDown1.Value = int.Parse(appConfigManager.GetTagValue(appConfigManager.daysBeforeDeadline));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveDbLocation();
            SaveOutputLocation();
            appConfigManager.ChangeTagValue(appConfigManager.clearEquipmentAfterAdding, checkBox1.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.clearDefectsAfterAdding, checkBox2.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.clearEmployeesAfterAdding, checkBox3.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.copyEmployeesToAllTypesOfMethods, checkBox4.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.userIsReviewer, checkBox5.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.hideControlMethods, checkBox6.Checked.ToString());
            if (checkBox6.Checked)
            {
                parent.HideControlMethodTab();
            }
            else
            {
                parent.ShowControlMethodTab();
            }
            appConfigManager.ChangeTagValue(appConfigManager.daysBeforeDeadline, numericUpDown1.Value.ToString());
            Close();
        }

        private void SaveOutputLocation()
        {
            appConfigManager.ChangeOutputLocation(textBox2.Text);
            parent.SetOutputLocation();
            Close();
        }


        private void SaveDbLocation()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            if (appConfigManager.GetAttachDbFileName() != textBox1.Text)
            {
                builder.AttachDBFilename = textBox1.Text;
                appConfigManager.ChangeConnectionString(builder.ConnectionString);
                MessageBox.Show("База данных изменена успешно. Необходимо перезапустить программу.", "Оповещение");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string path = fbd.SelectedPath;
                    textBox2.Text = path;
                }
            }
        }

        private void StartProgressBar(string message, int max, int step)
        {
            label6.Visible = true;
            label6.Text = message;
            progressBar1.Visible = true;
            progressBar1.Maximum = max;
            progressBar1.Step = step;
            progressBar1.Value = 0;
        }

        private void StopProgressBar()
        {
            progressBar1.Visible = false;
            label6.Visible = false;
        }

        private void ProgressBarStep(string message)
        {
            label6.Text = message;
            progressBar1.PerformStep();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    ServiceDB_Integration service = new ServiceDB_Integration(integrateConnectionString.Replace("<path>", openFileDialog1.FileName));

                    var integratingControlNames = service.Set<ControlName>().Select(en => en).ToList();
                    var existedControlNames = uow.ControlNames.GetAllOrm().ToList();
                    string message = "Невозможно загрузить базу данных. Разные версии программ.";
                    bool isNamesFit = true;
                    if (integratingControlNames.Count() != existedControlNames.Count())
                    {
                        isNamesFit = false;
                    }
                    else
                    {
                        for (int i = 0; i < existedControlNames.Count(); i++)
                        {
                            if (existedControlNames[i].name != integratingControlNames[i].name)
                            {
                                isNamesFit = false;
                                break;
                            }
                        }
                    }

                    if (isNamesFit)
                    {
                        StartProgressBar("Интеграция базы данных...", 12, 1);

                        ProgressBarStep("Копирование промышленных объектов..."); IntegrateIndustrialObjects(service);
                        ProgressBarStep("Копирование материалов..."); IntegrateMaterials(service);
                        ProgressBarStep("Копирование заказчиков..."); IntegrateCustomers(service);
                        ProgressBarStep("Копирование разработчиков чертежей..."); IntegrateScheduleOrganizations(service);
                        ProgressBarStep("Копирование методической документации..."); IntegrateControlMethodDocumentations(service);
                        ProgressBarStep("Копирование сотрудников..."); IntegrateEmployees(service);
                        ProgressBarStep("Копирование пользователей..."); IntegrateUsers(service);
                        ProgressBarStep("Копирование оборудования..."); IntegrateEquipments(service);
                        ProgressBarStep("Копирование нормативной документации..."); IntegrateRequirementDocumentations(service);
                        ProgressBarStep("Копирование шаблонов сборочных единиц..."); IntegrateTemplates(service);
                        ProgressBarStep("Копирование деталей..."); IntegrateComponents(service);
                        ProgressBarStep("Копирование журнала контроля..."); IntegrateJournals(service);

                        StopProgressBar();
                        MessageBox.Show("Интеграция базы данных успешно завершена!");
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке базы данных! " + ex.Message);
            }
        }

        private void IntegrateJournals(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Journal>().Select(en => en);
            var existedEntities = uow.Journals.GetAllOrm();
            IJournalService journalService = new JournalService(uow);
            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.requestNumber == existedEntity.requestNumber) )
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    var component = testEntity.Component;
                    var indOb = testEntity.IndustrialObject; ;
                    int cml_id = CreateControlMethodsLib(testEntity.ControlMethodsLib, service);
                    var contract = testEntity.Contract;
                    var customer = testEntity.Customer;
                    var schOrg = testEntity.ScheduleOrganization;
                    var material = testEntity.Material;
                    var user = testEntity.User;
                    var createdJournal = uow.Journals.Create(new Journal
                    {
                        amount = testEntity.amount,
                        description = testEntity.description,
                        modifiedDate = testEntity.modifiedDate,
                        controlDate = testEntity.controlDate,
                        requestNumber = testEntity.requestNumber,
                        userModifierLogin = testEntity.userModifierLogin,
                        weight = testEntity.weight,
                        industrialObject_id = indOb != null ? (int?)uow.IndustrialObjects.GetOrmIndustrialObjectByName(indOb.name).id : null,
                        component_id = component != null ? (int?)uow.Components.GetOrmComponentByNameAndPressmark(component.name, component.pressmark).id : null,
                        contract_id = contract != null ? (int?)uow.Contracts.GetOrmContractByName(contract.name).id : null,
                        customer_id = customer != null ? (int?)uow.Customers.GetOrmCustomerByName(customer.organization).id : null,
                        material_id = material != null ? (int?)uow.Materials.GetOrmMaterialByName(material.name).id : null,
                        scheduleOrganization_id =schOrg != null ? (int?)uow.ScheduleOrganizations.GetOrmScheduleOrganizationByName(schOrg.name).id : null,
                        userOwner_id = user != null ? (int?)uow.Users.GetUserByLogin(user.login).Id : null,
                        controlMethodsLib_id = cml_id,
                    });
                    uow.Commit();

                    parent.AddNewJournal(journalService.GetLiteJournal(createdJournal.id));
                }

            }

        }

        private void IntegrateComponents(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Component>().Select(en => en);
            var existedEntities = uow.Components.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name) && (testEntity.pressmark == existedEntity.pressmark))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    var indOb = testEntity.IndustrialObject;
                    var template = service.Set<Template>().FirstOrDefault(e => e.id == testEntity.template_id);
                    uow.Components.Create(new Component
                    {
                        name = testEntity.name,
                        pressmark = testEntity.pressmark,
                        Count = testEntity.Count,
                        Description = testEntity.Description,
                        industrialObject_id = indOb != null ? (int?)uow.IndustrialObjects.GetIndustrialObjectByName(indOb.name).Id : null,
                        template_id = template != null ? (int?)uow.Templates.GetTemplateByName(template.name).Id : null,
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateIndustrialObjects(ServiceDB_Integration service)
        {
            var testEntities = service.Set<IndustrialObject>().Select(en => en);
            var existedEntities = uow.IndustrialObjects.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if (testEntity.name == existedEntity.name)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.IndustrialObjects.Create(new IndustrialObject
                    {
                        name = testEntity.name,
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateUsers(ServiceDB_Integration service)
        {
            var testEntities = service.Set<User>().Select(en => en);
            var existedEntities = uow.Users.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if (testEntity.login == existedEntity.login)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    var employee = service.Set<Employee>().FirstOrDefault(e => e.id == testEntity.employee_id);
                    uow.Users.Create(new User
                    {
                        login = testEntity.login,
                        password = testEntity.password,
                        role_id = testEntity.role_id,
                        modifiedDate = testEntity.modifiedDate,
                        employee_id = employee != null ? (int?)uow.Employees.GetOrmEmployeeByFio(employee.sirname, employee.name, employee.fathername).id : null,
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateMaterials(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Material>().Select(en => en);
            var existedEntities = uow.Materials.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name) )
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.Materials.Create(new Material
                    {
                        name = testEntity.name,
                        description = testEntity.description
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateCustomers(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Customer>().Select(en => en);
            var existedEntities = uow.Customers.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.organization == existedEntity.organization))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    var ormLib = uow.ContractLibs.Create(new ContractLib());
                    uow.Commit();
                    var contracts = service.Set<Contract>().Where(en => en.contractLib_id == testEntity.contractLib_id);
                    foreach(var contract in contracts)
                    {
                        contract.contractLib_id = ormLib.id;
                        Contract newContract = new Contract {
                            contractLib_id = ormLib.id,
                            beginDate = contract.beginDate,
                            endDate = contract.endDate,
                            name = contract.name
                        };
                        uow.Contracts.Create(newContract);
                    }

                    uow.Customers.Create(new Customer
                    {
                        organization = testEntity.organization,
                        address = testEntity.address,
                        phone = testEntity.phone,
                        contractLib_id = ormLib.id,                        
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateScheduleOrganizations(ServiceDB_Integration service)
        {
            var testEntities = service.Set<ScheduleOrganization>().Select(en => en);
            var existedEntities = uow.ScheduleOrganizations.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.ScheduleOrganizations.Create(new ScheduleOrganization
                    {
                        name = testEntity.name,
                        description = testEntity.description,
                        address = testEntity.address,
                        
                    });
                }

            }
            uow.Commit();
        }

        private int CreateControlMethodsLib(ControlMethodsLib lib, ServiceDB_Integration service)
        {
            var ormLib = uow.ControlMethodsLibs.Create(new ControlMethodsLib());
            uow.Commit();
            var controls = service.Set<ORM.Control>().Where(en => en.controlMethodsLib_id == lib.id);
            foreach (var oldcontrol in controls)
            {
                ORM.Control control = new ORM.Control();
                control.controlMethodsLib_id = ormLib.id;
                Employee chief = null;
                if (oldcontrol.chiefEmployee_id != null)
                {
                    chief = oldcontrol.Employee;
                    control.chiefEmployee_id = uow.Employees.GetOrmEmployeeByFio(chief.sirname, chief.name, chief.fathername).id;
                }

                var ormDocLib = uow.ControlMethodDocumentationLibs.Create(new ControlMethodDocumentationLib());
                uow.Commit();
                var selectedDocs = service.Set<SelectedControlMethodDocumentation>().Where(e => e.lib_id == oldcontrol.controlMethodDocumentationLib_id);
                foreach (var sd in selectedDocs)
                {
                    var doc = sd.ControlMethodDocumentation; 
                    SelectedControlMethodDocumentation newsd = new SelectedControlMethodDocumentation();
                    newsd.lib_id = ormDocLib.id;
                    newsd.entity_id = uow.ControlMethodDocumentations.GetControlMethodDocumentationByName(doc.name).Id;
                    uow.SelectedControlMethodDocumentations.Create(newsd);
                }
                control.controlMethodDocumentationLib_id = ormDocLib.id;

                var ormEmpLib = uow.EmployeeLibs.Create(new EmployeeLib());
                uow.Commit();
                var selectedEmps = service.Set<SelectedEmployee>().Where(e => e.lib_id == oldcontrol.employeeLib_id);
                foreach (var sd in selectedEmps)
                {
                    var doc = sd.Employee;
                    SelectedEmployee newsd = new SelectedEmployee();
                    newsd.lib_id = ormEmpLib.id;
                    newsd.entity_id = uow.Employees.GetOrmEmployeeByFio(doc.sirname, doc.name, doc.fathername).id;
                    uow.SelectedEmployees.Create(newsd);
                }
                control.employeeLib_id = ormEmpLib.id;

                var ormEqLib = uow.EquipmentLibs.Create(new EquipmentLib());
                uow.Commit();
                var selectedEqs = service.Set<SelectedEquipment>().Where(e => e.lib_id == oldcontrol.equipmentLib_id);
                foreach (var sd in selectedEqs)
                {
                    var doc = sd.Equipment;
                    SelectedEquipment newsd = new SelectedEquipment();
                    newsd.lib_id = ormEqLib.id;
                    newsd.entity_id = uow.Equipments.GetEquipmentByName(doc.name).Id;
                    uow.SelectedEquipments.Create(newsd);
                }
                control.equipmentLib_id = ormEqLib.id;

                var ormRdLib = uow.RequirementDocumentationLibs.Create(new RequirementDocumentationLib());
                uow.Commit();
                var selectedRd = service.Set<SelectedRequirementDocumentation>().Where(e => e.lib_id == oldcontrol.requirementDocumentationLib_id);
                foreach (var sd in selectedRd)
                {
                    var doc = sd.RequirementDocumentation;
                    SelectedRequirementDocumentation newsd = new SelectedRequirementDocumentation();
                    newsd.lib_id = ormRdLib.id;
                    newsd.entity_id = uow.RequirementDocumentations.GetRequirementDocumentationByName(doc.name).Id;
                    uow.SelectedRequirementDocumentations.Create(newsd);
                }
                control.requirementDocumentationLib_id = ormRdLib.id;

                var imgLib = uow.ImageLibs.Create(new ImageLib());
                uow.Commit();
                var imgs = service.Set<ORM.Image>().Where(e => e.imageLib_id == oldcontrol.imageLib_id);
                foreach (var sd in imgs)
                {
                    ORM.Image newsd = new ORM.Image();
                    newsd.imageLib_id = imgLib.id;
                    newsd.image = sd.image;
                    uow.Images.Create(newsd);
                }
                control.imageLib_id = imgLib.id;

                var resLib = uow.ResultLibs.Create(new ResultLib());
                uow.Commit();
                var rests = service.Set<Result>().Where(e => e.resultLib_id == oldcontrol.resultLib_id);
                foreach (var sd in rests)
                {
                    Result newsd = new Result();
                    newsd.resultLib_id = resLib.id;
                    newsd.defectDescription = sd.defectDescription;
                    newsd.norm = sd.norm;
                    newsd.number = sd.number;
                    newsd.quality = sd.quality;
                    newsd.welder = sd.welder;
                    newsd.weldingType = sd.weldingType;
                    uow.Results.Create(newsd);
                }
                control.resultLib_id = resLib.id;

                control.controlName_id = oldcontrol.controlName_id;
                control.isControlled = oldcontrol.isControlled;
                control.additionally = oldcontrol.additionally;
                control.light = oldcontrol.light;
                control.number = oldcontrol.number;
                control.protocolNumber = oldcontrol.protocolNumber;
                control.temperature = oldcontrol.temperature;

                uow.Controls.Create(control);
            }

            return ormLib.id;
        }

        private void IntegrateTemplates(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Template>().Select(en => en);
            var existedEntities = uow.Templates.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    int cml_id = CreateControlMethodsLib(testEntity.ControlMethodsLib, service);
                    var contract = testEntity.Contract;
                    var customer = testEntity.Customer;
                    var indOb = testEntity.IndustrialObject;
                    var schOrg = testEntity.ScheduleOrganization;
                    var material = service.Set<Material>().FirstOrDefault(e => e.id == testEntity.material_id.Value);
                    uow.Templates.Create(new Template
                    {
                        name = testEntity.name,
                        description = testEntity.description,
                        amount = testEntity.amount,
                        controlDate = testEntity.controlDate,
                        weight = testEntity.weight,
                        requestNumber = testEntity.requestNumber,
                        contract_id = contract != null ? (int?)uow.Contracts.GetOrmContractByName(contract.name).id : null,
                        customer_id = customer != null ? (int?)uow.Customers.GetOrmCustomerByName(testEntity.Customer.organization).id : null,
                        industrialObject_id = indOb != null ? (int?)uow.IndustrialObjects.GetOrmIndustrialObjectByName(indOb.name).id : null,
                        scheduleOrganization_id = schOrg != null ? (int?)uow.ScheduleOrganizations.GetOrmScheduleOrganizationByName(schOrg.name).id : null,
                        material_id = material != null ? (int?)uow.Materials.GetOrmMaterialByName(material.name).id : null,
                        controlMethodsLib_id = cml_id,      
                
                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateControlMethodDocumentations(ServiceDB_Integration service)
        {
            var testEntities = service.Set<ControlMethodDocumentation>().Select(en => en);
            var existedEntities = uow.ControlMethodDocumentations.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.ControlMethodDocumentations.Create(new ControlMethodDocumentation
                    {
                        name = testEntity.name,
                        controlName_id = testEntity.controlName_id,
                        pressmark = testEntity.pressmark,
                        

                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateEmployees(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Employee>().Select(en => en);
            var existedEntities = uow.Employees.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name) && (testEntity.sirname == existedEntity.sirname) && (testEntity.fathername == existedEntity.fathername))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.Employees.Create(new Employee
                    {
                        name = testEntity.name,
                        sirname = testEntity.sirname,
                        fathername = testEntity.fathername,
                        medicalCheckDate = testEntity.medicalCheckDate,
                        knowledgeCheckDate = testEntity.knowledgeCheckDate,
                        function = testEntity.function,

                    });
                }

            }
            uow.Commit();
        }


        private void IntegrateEquipments(ServiceDB_Integration service)
        {
            var testEntities = service.Set<Equipment>().Select(en => en);
            var existedEntities = uow.Equipments.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name) && (testEntity.factoryNumber == existedEntity.factoryNumber))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.Equipments.Create(new Equipment
                    {
                        name = testEntity.name,
                        checkDate = testEntity.checkDate,
                        isChecked = testEntity.isChecked,
                        factoryNumber = testEntity.factoryNumber,
                        nextTechnicalCheckDate = testEntity.nextTechnicalCheckDate,
                        numberOfTechnicalCheck = testEntity.numberOfTechnicalCheck,
                        technicalCheckDate = testEntity.technicalCheckDate,
                        pressmark = testEntity.pressmark,
                        type = testEntity.type

                    });
                }

            }
            uow.Commit();
        }

        private void IntegrateRequirementDocumentations(ServiceDB_Integration service)
        {
            var testEntities = service.Set<RequirementDocumentation>().Select(en => en);
            var existedEntities = uow.RequirementDocumentations.GetAllOrm();

            foreach (var testEntity in testEntities)
            {
                bool isExist = false;
                foreach (var existedEntity in existedEntities)
                {
                    if ((testEntity.name == existedEntity.name) && (testEntity.pressmark == existedEntity.pressmark))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    uow.RequirementDocumentations.Create(new RequirementDocumentation
                    {
                        name = testEntity.name,
                        objectGroup = testEntity.objectGroup,
                        pressmark = testEntity.pressmark,

                    });
                }

            }
            uow.Commit();
        }


    }

}
