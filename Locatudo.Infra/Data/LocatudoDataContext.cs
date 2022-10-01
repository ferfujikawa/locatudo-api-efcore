using Locatudo.Domain.Entities;
using Locatudo.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Locatudo.Infra.Data
{
    public class LocatudoDataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public LocatudoDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentMap());
        }
    }
}
