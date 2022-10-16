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

            builder.Property(x => x.EquipmentId)
                .HasColumnName("equipment_id")
                .HasColumnType("uuid")
                .IsRequired();

            builder.HasOne(x => x.Equipment)
                .WithMany()
                .HasForeignKey(x => x.EquipmentId)
                .IsRequired();

            builder.Property(x => x.TenantId)
                .HasColumnName("tenant_id")
                .HasColumnType("uuid")
                .IsRequired();

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
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

            builder.Property(x => x.AppraiserId)
                .HasColumnName("appraiser_id")
                .HasColumnType("uuid");

            builder.HasOne(x => x.Appraiser)
                .WithMany()
                .HasForeignKey(x => x.AppraiserId);
        }
    }
}
