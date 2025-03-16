using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class MaterialTrackingConfiguration : IEntityTypeConfiguration<MaterialTracking>
    {
        public void Configure(EntityTypeBuilder<MaterialTracking> entity)
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.CserialId).HasColumnName("CSerialId");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.MaterialTracking)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_MaterialTracking_Company");

            entity.HasOne(d => d.Cserial)
                .WithMany(p => p.MaterialTracking)
                .HasForeignKey(d => d.CserialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaterialTracking_ComponentSerial");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.MaterialTracking)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_MaterialTracking_Site");

            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.MaterialTracking)
                .HasForeignKey(d => d.TeamLeaderId)
                .HasConstraintName("FK_MaterialTracking_AccountSSOM");
        }
    }
}
