using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class TransactionInfoConfiguration : IEntityTypeConfiguration<TransactionInfo>
    {
        public void Configure(EntityTypeBuilder<TransactionInfo> entity)
        {
            entity.HasKey(e => e.TransCode).HasName("PK__TRANSACT__4BCD394CB11E2C8D");

            entity.ToTable("TRANSACTION_INFO");

            entity.Property(e => e.TransCode)
                .ValueGeneratedNever()
                .HasColumnName("trans_code");
            entity.Property(e => e.AuditChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("audit_change_date");
            entity.Property(e => e.CommerceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("commerce_id");
            entity.Property(e => e.TransConcept)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("trans_concept");
            entity.Property(e => e.TransDate).HasColumnName("trans_date");
            entity.Property(e => e.TransPaymentMethod).HasColumnName("trans_payment_method");
            entity.Property(e => e.TransStatus).HasColumnName("trans_status");
            entity.Property(e => e.TransTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("trans_total");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Commerce).WithMany(p => p.TransactionInfos)
                .HasForeignKey(d => d.CommerceId)
                .HasConstraintName("FK__TRANSACTI__comme__0F624AF8");

            entity.HasOne(d => d.TransPaymentMethodNavigation).WithMany(p => p.TransactionInfos)
                .HasForeignKey(d => d.TransPaymentMethod)
                .HasConstraintName("FK__TRANSACTI__trans__10566F31");

            entity.HasOne(d => d.TransStatusNavigation).WithMany(p => p.TransactionInfos)
                .HasForeignKey(d => d.TransStatus)
                .HasConstraintName("FK__TRANSACTI__trans__114A936A");

            entity.HasOne(d => d.User).WithMany(p => p.TransactionInfos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TRANSACTI__user___0E6E26BF");

        }
    }
}
