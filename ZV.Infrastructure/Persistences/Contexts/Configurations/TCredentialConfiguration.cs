using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class TCredentialConfiguration : IEntityTypeConfiguration<TCredential>
    {
        public void Configure(EntityTypeBuilder<TCredential> entity)
        {
                entity.HasKey(e => e.CredentialId).HasName("PK__T_CREDEN__31AC61B516515838");

                entity.ToTable("T_CREDENTIALS");

                entity.HasIndex(e => e.Username, "UQ__T_CREDEN__F3DBC572C7620F35").IsUnique();

                entity.Property(e => e.CredentialId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("credential_id");
                entity.Property(e => e.Pass)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("pass");
                entity.Property(e => e.Salt)
                    .HasMaxLength(128)
                    .HasColumnName("salt");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Credential).WithOne(p => p.TCredential)
                    .HasForeignKey<TCredential>(d => d.CredentialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_CREDENT__crede__08B54D69");
            }
    }
}
