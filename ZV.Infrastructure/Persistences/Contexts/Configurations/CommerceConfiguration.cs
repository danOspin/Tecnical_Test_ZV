using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class CommerceConfiguration : IEntityTypeConfiguration<Commerce>
    {
        public void Configure(EntityTypeBuilder<Commerce> entity)
        {
            entity.HasKey(e => e.CommerceId).HasName("PK__COMMERCE__C723A1434F3C3C06");

            entity.ToTable("COMMERCE");

            entity.HasIndex(e => e.Nit, "UQ__COMMERCE__DF97D0E4A35D5892").IsUnique();

            entity.Property(e => e.CommerceId)
                .ValueGeneratedNever()
                .HasColumnName("commerce_id");
            entity.Property(e => e.CommerceAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commerce_address");
            entity.Property(e => e.CommerceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commerce_name");
            entity.Property(e => e.CommerceStatus).HasColumnName("commerce_status");
            entity.Property(e => e.Nit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nit");
        }
    }
}
