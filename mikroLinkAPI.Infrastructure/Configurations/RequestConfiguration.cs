using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;


namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> entity)
        {
            entity.Property(e => e.RequestDate).HasColumnType("datetime");

            entity.Property(e => e.RequestMessage).HasMaxLength(250);

            entity.Property(e => e.WorkOrderNo)
                .IsRequired()
            .HasMaxLength(30);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.RequestCompany)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Request_Company");

            entity.HasOne(d => d.ReceiverCompany)
                .WithMany(p => p.RequestReceiverCompany)
                .HasForeignKey(d => d.ReceiverCompanyId)
                .HasConstraintName("FK_Request_Company1");

            entity.HasOne(d => d.ReceiverSite)
                .WithMany(p => p.RequestReceiverSite)
                .HasForeignKey(d => d.ReceiverSiteId)
                .HasConstraintName("FK_Request_Site");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.RequestSite)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_Request_Site1");

            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.RequestTeamLeader)
                .HasForeignKey(d => d.TeamLeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_AccountSSOM");

            entity.HasOne(d => d.WhoCanceled)
                .WithMany(p => p.RequestWhoCanceled)
                .HasForeignKey(d => d.WhoCanceledId)
                .HasConstraintName("FK_Request_AccountSSOM1");

            entity.HasOne(d => d.WhoDone)
                .WithMany(p => p.RequestWhoDone)
                .HasForeignKey(d => d.WhoDoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_AccountSSOM2");
        }
    }
}
