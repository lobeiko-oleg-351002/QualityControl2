namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ServiceDB : DbContext
    {
        public ServiceDB()
            : base("name=ServiceDB")
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateLib> CertificateLibs { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<ComponentLib> ComponentLibs { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractLib> ContractLibs { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<ControlMethodDocumentation> ControlMethodDocumentations { get; set; }
        public virtual DbSet<ControlMethodDocumentationLib> ControlMethodDocumentationLibs { get; set; }
        public virtual DbSet<ControlMethodsLib> ControlMethodsLibs { get; set; }
        public virtual DbSet<ControlName> ControlNames { get; set; }
        public virtual DbSet<ControlNameLib> ControlNameLibs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeLib> EmployeeLibs { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentLib> EquipmentLibs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageLib> ImageLibs { get; set; }
        public virtual DbSet<IndustrialObject> IndustrialObjects { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Raw> Raws { get; set; }
        public virtual DbSet<RequirementDocumentation> RequirementDocumentations { get; set; }
        public virtual DbSet<RequirementDocumentationLib> RequirementDocumentationLibs { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultLib> ResultLibs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScheduleOrganization> ScheduleOrganizations { get; set; }
        public virtual DbSet<SelectedCertificate> SelectedCertificates { get; set; }
        public virtual DbSet<SelectedComponent> SelectedComponents { get; set; }
        public virtual DbSet<SelectedControlMethodDocumentation> SelectedControlMethodDocumentations { get; set; }
        public virtual DbSet<SelectedControlName> SelectedControlNames { get; set; }
        public virtual DbSet<SelectedEmployee> SelectedEmployees { get; set; }
        public virtual DbSet<SelectedEquipment> SelectedEquipments { get; set; }
        public virtual DbSet<SelectedRequirementDocumentation> SelectedRequirementDocumentations { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeldJoint> WeldJoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>()
                .HasMany(e => e.SelectedCertificate)
                .WithRequired(e => e.Certificate)
                .HasForeignKey(e => e.entity_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CertificateLib>()
                .HasMany(e => e.SelectedCertificate)
                .WithRequired(e => e.CertificateLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<Component>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Component)
                .HasForeignKey(e => e.component_id);

            modelBuilder.Entity<Component>()
                .HasMany(e => e.SelectedComponent)
                .WithRequired(e => e.Component)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<ComponentLib>()
                .HasMany(e => e.SelectedComponent)
                .WithRequired(e => e.ComponentLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.contract_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.contract_id);

            modelBuilder.Entity<ContractLib>()
                .HasMany(e => e.Contract)
                .WithOptional(e => e.ContractLib)
                .HasForeignKey(e => e.contractLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ContractLib>()
                .HasMany(e => e.Customer)
                .WithOptional(e => e.ContractLib)
                .HasForeignKey(e => e.contractLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodDocumentation>()
                .HasMany(e => e.SelectedControlMethodDocumentation)
                .WithRequired(e => e.ControlMethodDocumentation)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<ControlMethodDocumentationLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlMethodDocumentationLib)
                .HasForeignKey(e => e.controlMethodDocumentationLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodDocumentationLib>()
                .HasMany(e => e.SelectedControlMethodDocumentation)
                .WithRequired(e => e.ControlMethodDocumentationLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<ControlMethodsLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlMethodsLib)
                .HasForeignKey(e => e.controlMethodsLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodsLib>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.ControlMethodsLib)
                .HasForeignKey(e => e.controlMethodsLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodsLib>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.ControlMethodsLib)
                .HasForeignKey(e => e.controlMethodsLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.Certificate)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.controlName_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.controlName_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.ControlMethodDocumentation)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.controlName_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.SelectedControlName)
                .WithRequired(e => e.ControlName)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<ControlNameLib>()
                .HasMany(e => e.SelectedControlName)
                .WithRequired(e => e.ControlNameLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Certificate)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.employee_id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.chiefEmployee_id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SelectedEmployee)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<EmployeeLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.EmployeeLib)
                .HasForeignKey(e => e.employeeLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EmployeeLib>()
                .HasMany(e => e.SelectedEmployee)
                .WithRequired(e => e.EmployeeLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.SelectedEquipment)
                .WithRequired(e => e.Equipment)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<EquipmentLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.EquipmentLib)
                .HasForeignKey(e => e.equipmentLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EquipmentLib>()
                .HasMany(e => e.SelectedEquipment)
                .WithRequired(e => e.EquipmentLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<ImageLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ImageLib)
                .HasForeignKey(e => e.imageLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ImageLib>()
                .HasMany(e => e.Image)
                .WithOptional(e => e.ImageLib)
                .HasForeignKey(e => e.imageLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<IndustrialObject>()
                .HasMany(e => e.Component)
                .WithOptional(e => e.IndustrialObject)
                .HasForeignKey(e => e.industrialObject_id);

            modelBuilder.Entity<IndustrialObject>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.IndustrialObject)
                .HasForeignKey(e => e.industrialObject_id);

            modelBuilder.Entity<IndustrialObject>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.IndustrialObject)
                .HasForeignKey(e => e.industrialObject_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Material>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Material)
                .HasForeignKey(e => e.material_id);

            modelBuilder.Entity<RequirementDocumentation>()
                .HasMany(e => e.SelectedRequirementDocumentation)
                .WithRequired(e => e.RequirementDocumentation)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<RequirementDocumentationLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.RequirementDocumentationLib)
                .HasForeignKey(e => e.requirementDocumentationLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RequirementDocumentationLib>()
                .HasMany(e => e.SelectedRequirementDocumentation)
                .WithRequired(e => e.RequirementDocumentationLib)
                .HasForeignKey(e => e.lib_id);

            modelBuilder.Entity<ResultLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ResultLib)
                .HasForeignKey(e => e.resultLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ResultLib>()
                .HasMany(e => e.Result)
                .WithOptional(e => e.ResultLib)
                .HasForeignKey(e => e.resultLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<ScheduleOrganization>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.ScheduleOrganization)
                .HasForeignKey(e => e.scheduleOrganization_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ScheduleOrganization>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.ScheduleOrganization)
                .HasForeignKey(e => e.scheduleOrganization_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.userOwner_id);

            modelBuilder.Entity<WeldJoint>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.WeldJoint)
                .HasForeignKey(e => e.weldJoint_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WeldJoint>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.WeldJoint)
                .HasForeignKey(e => e.weldJoint_id)
                .WillCascadeOnDelete();
        }
    }
}
