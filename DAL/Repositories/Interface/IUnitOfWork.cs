using DAL.Entities.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUnitOfWork
    {   
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        ICertificateRepository Certificates { get; }
        IComponentRepository Components { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IEquipmentRepository Equipments { get; }
        IIndustrialObjectRepository IndustrialObjects { get; }
        IMaterialRepository Materials { get; }
        ITemplateRepository Templates { get; }
        IControlMethodDocumentationRepository ControlMethodDocumentations { get; }
        IRequirementDocumentationRepository RequirementDocumentations { get; }
        IControlNameRepository ControlNames { get; }
        IControlRepository Controls { get; }
        IImageRepository Images { get; }
        IJournalRepository Journals { get; }
        IResultRepository Results { get; }
        IWeldJointRepository WeldJoints { get; }
        IContractRepository Contracts { get; }
        ISelectedEntityRepository<SelectedCertificate> SelectedCertificates { get; }
        ISelectedEntityRepository<SelectedComponent> SelectedComponents { get; }
        ISelectedEntityRepository<SelectedControlMethodDocumentation> SelectedControlMethodDocumentations { get; }
        ISelectedEntityRepository<SelectedControlName> SelectedControlNames { get; }
        ISelectedEntityRepository<SelectedEmployee> SelectedEmployees { get; }
        ISelectedEntityRepository<SelectedEquipment> SelectedEquipments { get; }
        ISelectedEntityRepository<SelectedRequirementDocumentation> SelectedRequirementDocumentations { get; }
        IRepository<IDalEntityLib, ContractLib> ContractLibs { get; }
        IRepository<IDalEntityLib, ImageLib> ImageLibs { get; }
        IRepository<IDalEntityLib, ResultLib> ResultLibs { get; }
        IRepository<IDalEntityLib, CertificateLib> CertificateLibs { get; }
        IRepository<IDalEntityLib, ComponentLib> ComponentLibs { get; }
        IRepository<IDalEntityLib, ControlMethodDocumentationLib> ControlMethodDocumentationLibs { get; }
        IRepository<IDalEntityLib, ControlNameLib> ControlNameLibs { get; }
        IRepository<IDalEntityLib, EmployeeLib> EmployeeLibs { get; }
        IRepository<IDalEntityLib, EquipmentLib> EquipmentLibs { get; }
        IRepository<IDalEntityLib, RequirementDocumentationLib> RequirementDocumentationLibs { get; }
        IRepository<IDalEntityLib, ControlMethodsLib> ControlMethodsLibs { get; }

        void Commit();
    }
}
