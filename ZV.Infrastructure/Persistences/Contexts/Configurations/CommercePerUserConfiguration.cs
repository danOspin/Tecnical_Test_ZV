using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class CommercePerUserConfiguration : IEntityTypeConfiguration<CommercePerUser>
    {
        public void Configure(EntityTypeBuilder<CommercePerUser> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__COMMERCE__3213E83F163396E0");

            entity.ToTable("COMMERCE_PER_USER");

            entity.HasIndex(e => new { e.CommerceId, e.UserId }, "UQ__COMMERCE__2CB84232B10FD49F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommerceId).HasColumnName("commerce_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Commerce).WithMany(p => p.CommercePerUsers)
                .HasForeignKey(d => d.CommerceId)
                .HasConstraintName("FK__COMMERCE___comme__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.CommercePerUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__COMMERCE___user___5812160E");
        }
    }
}
