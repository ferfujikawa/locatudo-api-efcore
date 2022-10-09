using Locatudo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locatudo.Infra.Data.Mappings.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void MapUserAttributes<T>(this EntityTypeBuilder<T> builder) where T : User
        {
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.Name)
                .Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(x => x.Name)
                .Property(x => x.LastName)
                .HasColumnName("last_name")
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
