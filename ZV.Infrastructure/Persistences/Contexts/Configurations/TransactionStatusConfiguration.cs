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
    public class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> entity)
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Transact__3683B5318C573194");

            entity.ToTable("TransactionStatus");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");
        }
    }
}
