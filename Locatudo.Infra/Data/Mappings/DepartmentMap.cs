using Locatudo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locatudo.Infra.Data.Mappings
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Address)
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
