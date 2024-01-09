using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class TransactionInfoAuditConfiguration : IEntityTypeConfiguration<TransactionInfoAudit>
    {
        public void Configure(EntityTypeBuilder<TransactionInfoAudit> entity)
        {
            entity.HasKey(e => e.AuditId).HasName("PK__TRANSACT__5AF33E33E577B399");

            entity.ToTable("TRANSACTION_INFO_AUDIT");

            entity.Property(e => e.AuditId)
                .ValueGeneratedNever()
                .HasColumnName("audit_id");
            entity.Property(e => e.Annotation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("annotation");
            entity.Property(e => e.ChangeDate)
                .HasColumnType("datetime")
                .HasColumnName("change_date");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("changed_by");
            entity.Property(e => e.TransCode).HasColumnName("trans_code");
            entity.Property(e => e.TransPaymentMethod).HasColumnName("trans_payment_method");
            entity.Property(e => e.TransStatus).HasColumnName("trans_status");

            entity.HasOne(d => d.TransCodeNavigation).WithMany(p => p.TransactionInfoAudits)
                .HasForeignKey(d => d.TransCode)
                .HasConstraintName("FK_TransactionInfoAudit_TransactionInfo");

            entity.HasOne(d => d.TransPaymentMethodNavigation).WithMany(p => p.TransactionInfoAudits)
                .HasForeignKey(d => d.TransPaymentMethod)
                .HasConstraintName("FK__TRANSACTI__trans__151B244E");

            entity.HasOne(d => d.TransStatusNavigation).WithMany(p => p.TransactionInfoAudits)
                .HasForeignKey(d => d.TransStatus)
                .HasConstraintName("FK__TRANSACTI__trans__160F4887");
        }
    }
}
