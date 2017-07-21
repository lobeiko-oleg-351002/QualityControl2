using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public ServiceDB context { get; }

        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private CertificateRepository certificateRepository;
        private EmployeeRepository employeeRepository;
        private CustomerRepository customerRepository;
        private TemplateRepository templateRepository;
        private ComponentRepository componentRepository;
        private IndustrialObjectRepository industrialObjectRepository;
        private MaterialRepository materialRepository;
        private EquipmentRepository equipmentRepository;
        private ControlMethodDocumentationRepository controlMethodDocumentationRepository;
        private RequirementDocumentationRepository requirementDocumentationRepository;
        private ControlNameRepository controlNameRepository;
        private ControlRepository controlRepository;
        private ImageRepository imageRepository;
        private JournalRepository journalRepository;
        private ResultRepository resultRepository;
        private WeldJointRepository weldJointRepository;
        private ContractRepository contractRepository;
        private SelectedEntityRepository<SelectedCertificate> selectedCertificateRepository;
        private SelectedEntityRepository<SelectedComponent> selectedComponentRepository;
        private SelectedEntityRepository<SelectedControlMethodDocumentation> selectedControlMethodDocumentationRepository;
        private SelectedEntityRepository<SelectedControlName> selectedControlNameRepository;
        private SelectedEntityRepository<SelectedEmployee> selectedEmployeeRepository;
        private SelectedEntityRepository<SelectedEquipment> selectedEquipmentRepository;
        private SelectedEntityRepository<SelectedRequirementDocumentation> selectedRequirementDocumentationRepository;
        private EntityLibRepository<ContractLib> contractLibRepository;
        private EntityLibRepository<ImageLib> imageLibRepository;
        private EntityLibRepository<ResultLib> resultLibRepository;
        private EntityLibRepository<CertificateLib> certificateLibRepository;
        private EntityLibRepository<ComponentLib> componentLibRepository;
        private EntityLibRepository<ControlMethodDocumentationLib> controlMethodDocumentationLibRepository;
        private EntityLibRepository<ControlNameLib> controlNameLibRepository;
        private EntityLibRepository<EmployeeLib> employeeLibRepository;
        private EntityLibRepository<EquipmentLib> equipmentLibRepository;
        private EntityLibRepository<RequirementDocumentationLib> requirementDocumentationLibRepository;
        private EntityLibRepository<ControlMethodsLib> controlMethodsLibRepository;
        private RawRepository rawRepository;
        private ScheduleOrganizationRepository scheduleOrganizationRepository;

        public IRoleRepository Roles 
            => roleRepository ?? (roleRepository = new RoleRepository(context));
        public IUserRepository Users
            => userRepository ?? (userRepository = new UserRepository(context));
        public ICertificateRepository Certificates
            => certificateRepository ?? (certificateRepository = new CertificateRepository(context));
        public IEmployeeRepository Employees
            => employeeRepository ?? (employeeRepository = new EmployeeRepository(context));
        public ICustomerRepository Customers
            => customerRepository ?? (customerRepository = new CustomerRepository(context));
        public ITemplateRepository Templates
            => templateRepository ?? (templateRepository = new TemplateRepository(context));
        public IComponentRepository Components
            => componentRepository ?? (componentRepository = new ComponentRepository(context));
        public IIndustrialObjectRepository IndustrialObjects
            => industrialObjectRepository ?? (industrialObjectRepository = new IndustrialObjectRepository(context));
        public IMaterialRepository Materials
            => materialRepository ?? (materialRepository = new MaterialRepository(context));
        public IEquipmentRepository Equipments
            => equipmentRepository ?? (equipmentRepository = new EquipmentRepository(context));
        public IControlMethodDocumentationRepository ControlMethodDocumentations
            => controlMethodDocumentationRepository ?? (controlMethodDocumentationRepository = new ControlMethodDocumentationRepository(context));
        public IRequirementDocumentationRepository RequirementDocumentations
            => requirementDocumentationRepository ?? (requirementDocumentationRepository = new RequirementDocumentationRepository(context));
        public IControlNameRepository ControlNames
            => controlNameRepository ?? (controlNameRepository = new ControlNameRepository(context));
        public IControlRepository Controls
            => controlRepository ?? (controlRepository = new ControlRepository(context));
        public IImageRepository Images
            => imageRepository ?? (imageRepository = new ImageRepository(context));
        public IJournalRepository Journals
            => journalRepository ?? (journalRepository = new JournalRepository(context));
        public IResultRepository Results
            => resultRepository ?? (resultRepository = new ResultRepository(context));
        public IWeldJointRepository WeldJoints
            => weldJointRepository ?? (weldJointRepository = new WeldJointRepository(context));
        public IContractRepository Contracts
            => contractRepository ?? (contractRepository = new ContractRepository(context));
        public ISelectedEntityRepository<SelectedCertificate> SelectedCertificates
            => selectedCertificateRepository ?? (selectedCertificateRepository = new SelectedEntityRepository<SelectedCertificate>(context));
        public ISelectedEntityRepository<SelectedComponent> SelectedComponents
            => selectedComponentRepository ?? (selectedComponentRepository = new SelectedEntityRepository<SelectedComponent>(context));
        public ISelectedEntityRepository<SelectedControlMethodDocumentation> SelectedControlMethodDocumentations
            => selectedControlMethodDocumentationRepository ?? (selectedControlMethodDocumentationRepository = new SelectedEntityRepository<SelectedControlMethodDocumentation>(context));
        public ISelectedEntityRepository<SelectedControlName> SelectedControlNames
            => selectedControlNameRepository ?? (selectedControlNameRepository = new SelectedEntityRepository<SelectedControlName>(context));
        public ISelectedEntityRepository<SelectedEmployee> SelectedEmployees
            => selectedEmployeeRepository ?? (selectedEmployeeRepository = new SelectedEntityRepository<SelectedEmployee>(context));
        public ISelectedEntityRepository<SelectedEquipment> SelectedEquipments
            => selectedEquipmentRepository ?? (selectedEquipmentRepository = new SelectedEntityRepository<SelectedEquipment>(context));
        public ISelectedEntityRepository<SelectedRequirementDocumentation> SelectedRequirementDocumentations
            => selectedRequirementDocumentationRepository ?? (selectedRequirementDocumentationRepository = new SelectedEntityRepository<SelectedRequirementDocumentation>(context));
        public IRepository<IDalEntityLib, ContractLib> ContractLibs
            => contractLibRepository ?? (contractLibRepository = new EntityLibRepository<ContractLib>(context));
        public IRepository<IDalEntityLib, ImageLib> ImageLibs
            => imageLibRepository ?? (imageLibRepository = new EntityLibRepository<ImageLib>(context));
        public IRepository<IDalEntityLib, ResultLib> ResultLibs
            => resultLibRepository ?? (resultLibRepository = new EntityLibRepository<ResultLib>(context));
        public IRepository<IDalEntityLib, CertificateLib> CertificateLibs
            => certificateLibRepository ?? (certificateLibRepository = new EntityLibRepository<CertificateLib>(context));
        public IRepository<IDalEntityLib, ComponentLib> ComponentLibs
            => componentLibRepository ?? (componentLibRepository = new EntityLibRepository<ComponentLib>(context));
        public IRepository<IDalEntityLib, ControlMethodDocumentationLib> ControlMethodDocumentationLibs
            => controlMethodDocumentationLibRepository ?? (controlMethodDocumentationLibRepository = new EntityLibRepository<ControlMethodDocumentationLib>(context));
        public IRepository<IDalEntityLib, ControlNameLib> ControlNameLibs
            => controlNameLibRepository ?? (controlNameLibRepository = new EntityLibRepository<ControlNameLib>(context));
        public IRepository<IDalEntityLib, EmployeeLib> EmployeeLibs
            => employeeLibRepository ?? (employeeLibRepository = new EntityLibRepository<EmployeeLib>(context));
        public IRepository<IDalEntityLib, EquipmentLib> EquipmentLibs
            => equipmentLibRepository ?? (equipmentLibRepository = new EntityLibRepository<EquipmentLib>(context));
        public IRepository<IDalEntityLib, RequirementDocumentationLib> RequirementDocumentationLibs
            => requirementDocumentationLibRepository ?? (requirementDocumentationLibRepository = new EntityLibRepository<RequirementDocumentationLib>(context));
        public IRepository<IDalEntityLib, ControlMethodsLib> ControlMethodsLibs
            => controlMethodsLibRepository ?? (controlMethodsLibRepository = new EntityLibRepository<ControlMethodsLib>(context));
        public IRawRepository Raws
            => rawRepository ?? (rawRepository = new RawRepository(context));
        public IScheduleOrganizationRepository ScheduleOrganizations
            => scheduleOrganizationRepository ?? (scheduleOrganizationRepository = new ScheduleOrganizationRepository(context));

        public UnitOfWork(ServiceDB context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
