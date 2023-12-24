using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> entity)
        {
            entity.HasKey(e => e.CredentialId).HasName("PK__USER_CRE__31AC61B58AB0F3DC");

            entity.ToTable("USER_CREDENTIALS");

            entity.HasIndex(e => e.Username, "UQ__USER_CRE__F3DBC572861E097A").IsUnique();

            entity.Property(e => e.CredentialId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("credential_id");
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Credential).WithOne(p => p.UserCredential)
                .HasForeignKey<UserCredential>(d => d.CredentialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_CRED__crede__5070F446");
        }
    }
}
