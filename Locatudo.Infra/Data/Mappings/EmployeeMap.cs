using Locatudo.Domain.Entities;
using Locatudo.Infra.Data.Mappings.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locatudo.Infra.Data.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees", "public");

            builder.MapUserAttributes();

            builder.Property<Guid>("department_id")
                .HasColumnType("uuid");

            builder.HasOne(x => x.Departament)
                .WithMany()
                .HasForeignKey("department_id");
        }
    }
}
