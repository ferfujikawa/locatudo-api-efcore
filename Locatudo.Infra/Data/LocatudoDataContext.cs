using Locatudo.Domain.Entities;
using Locatudo.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Data
{
    public class LocatudoDataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Outsourced> Outsourceds { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public LocatudoDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentMap());
            modelBuilder.ApplyConfiguration(new EquipmentMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new OutsourcedMap());
            modelBuilder.ApplyConfiguration(new RentalMap());
        }
    }
}
