

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class StoreExitConfiguration : IEntityTypeConfiguration<StoreExit>
    {
        public void Configure(EntityTypeBuilder<StoreExit> entity)
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.CserialId).HasColumnName("CSerialId");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.StoreExitCompany)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_StoreExit_Company");

            entity.HasOne(d => d.CompanyIdExitNavigation)
                .WithMany(p => p.StoreExitCompanyIdExitNavigation)
                .HasForeignKey(d => d.CompanyIdExit)
                .HasConstraintName("FK_StoreExit_Company1");

            entity.HasOne(d => d.Cserial)
                .WithMany(p => p.StoreExit)
                .HasForeignKey(d => d.CserialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreExit_ComponentSerial");

            entity.HasOne(d => d.Request)
                .WithMany(p => p.StoreExit)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_StoreExit_Request");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.StoreExitSite)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_StoreExit_Site");

            entity.HasOne(d => d.SiteIdExitNavigation)
                .WithMany(p => p.StoreExitSiteIdExitNavigation)
                .HasForeignKey(d => d.SiteIdExit)
                .HasConstraintName("FK_StoreExit_Site1");

            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.StoreExitTeamLeader)
                .HasForeignKey(d => d.TeamLeaderId)
                .HasConstraintName("FK_StoreExit_AccountSSOM");

            entity.HasOne(d => d.TeamLeaderIdExitNavigation)
                .WithMany(p => p.StoreExitTeamLeaderIdExitNavigation)
                .HasForeignKey(d => d.TeamLeaderIdExit)
                .HasConstraintName("FK_StoreExit_AccountSSOM1");

            entity.HasOne(d => d.WhoDone)
              .WithMany(p => p.StoreExitWhoDone)
              .HasForeignKey(d => d.CreatedBy)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_StoreExit_AccountSSOM2");

        }
    }
}
