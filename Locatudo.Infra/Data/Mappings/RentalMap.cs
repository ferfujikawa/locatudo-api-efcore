using Locatudo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locatudo.Infra.Data.Mappings
{
    public class RentalMap : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals", "public");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Equipment)
                .WithMany()
                .HasForeignKey("equipment_id")
                .IsRequired();

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey("tenant_id")
                .IsRequired();

            builder.OwnsOne(x => x.Status)
                .Property(x => x.Value)
                .HasColumnName("status")
                .HasColumnType("INT")
                .IsRequired();

            builder.OwnsOne(x => x.Time)
                .Property(x => x.Start)
                .HasColumnName("time")
                .HasColumnType("DATE")
                .IsRequired();

            builder.HasOne(x => x.Appraiser)
                .WithMany()
                .HasForeignKey("appraiser_id");
        }
    }
}
