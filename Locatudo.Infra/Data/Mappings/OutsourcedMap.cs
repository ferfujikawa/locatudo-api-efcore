using Locatudo.Domain.Entities;
using Locatudo.Infra.Data.Mappings.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locatudo.Infra.Data.Mappings
{
    public class OutsourcedMap : IEntityTypeConfiguration<Outsourced>
    {
        public void Configure(EntityTypeBuilder<Outsourced> builder)
        {
            builder.ToTable("outsourceds", "public");

            builder.MapUserAttributes();

            builder.OwnsOne(x => x.EnterpriseName)
                .Property(x => x.CompanyName)
                .HasColumnName("company_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
