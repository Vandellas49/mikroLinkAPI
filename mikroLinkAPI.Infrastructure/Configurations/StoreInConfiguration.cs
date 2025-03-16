using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class StoreInConfiguration : IEntityTypeConfiguration<StoreIn>
    {
        public void Configure(EntityTypeBuilder<StoreIn> entity)
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.CserialId).HasColumnName("CSerialId");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.StoreInCompany)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_StoreIn_Company");

            entity.HasOne(d => d.Cserial)
                .WithMany(p => p.StoreIn)
                .HasForeignKey(d => d.CserialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreIn_ComponentSerial");

            entity.HasOne(d => d.FromCompany)
                .WithMany(p => p.StoreInFromCompany)
                .HasForeignKey(d => d.FromCompanyId)
                .HasConstraintName("FK_StoreIn_Company1");

            entity.HasOne(d => d.FromSite)
                .WithMany(p => p.StoreInFromSite)
                .HasForeignKey(d => d.FromSiteId)
                .HasConstraintName("FK_StoreIn_Site1");

            entity.HasOne(d => d.FromTeamLeader)
                .WithMany(p => p.StoreInFromTeamLeader)
                .HasForeignKey(d => d.FromTeamLeaderId)
                .HasConstraintName("FK_StoreIn_AccountSSOM");

            entity.HasOne(d => d.Request)
                .WithMany(p => p.StoreIn)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_StoreIn_Request");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.StoreInSite)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_StoreIn_Site");

            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.StoreInTeamLeader)
                .HasForeignKey(d => d.TeamLeaderId)
                .HasConstraintName("FK_StoreIn_AccountSSOM1");

            entity.HasOne(d => d.WhoDone)
                .WithMany(p => p.StoreInWhoDone)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreIn_AccountSSOM2");
        }
    }
}
