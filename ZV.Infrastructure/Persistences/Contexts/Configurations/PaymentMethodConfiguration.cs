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
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> entity)
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__8A3EA9EBCD2F0480");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
        }
    }
}
