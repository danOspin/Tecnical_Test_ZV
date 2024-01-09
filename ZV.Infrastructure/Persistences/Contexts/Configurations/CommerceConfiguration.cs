using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class CommerceConfiguration : IEntityTypeConfiguration<Commerce>
    {
        public void Configure(EntityTypeBuilder<Commerce> entity)
        {
            entity.HasKey(e => e.CommerceId).HasName("PK__COMMERCE__C723A1436988D23C");

            entity.ToTable("COMMERCE");

            entity.Property(e => e.CommerceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("commerce_id");
            entity.Property(e => e.CommerceAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commerce_address");
            entity.Property(e => e.CommerceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commerce_name");
            entity.Property(e => e.CommerceNit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commerce_nit");
            entity.Property(e => e.CommerceStatus).HasColumnName("commerce_status");

            entity.HasOne(d => d.CommerceNavigation).WithOne(p => p.Commerce)
                .HasForeignKey<Commerce>(d => d.CommerceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMMERCE__commer__0B91BA14");
        }
    }
}
