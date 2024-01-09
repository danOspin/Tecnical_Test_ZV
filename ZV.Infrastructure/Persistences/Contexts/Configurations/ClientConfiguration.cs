using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Contexts.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.HasKey(e => e.ClientId).HasName("PK__CLIENT__BF21A4249A1DB7F0");

            entity.ToTable("CLIENT");

            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.ClientStatus).HasColumnName("client_status");
            entity.Property(e => e.ClientType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_type");
        }
    }
}
