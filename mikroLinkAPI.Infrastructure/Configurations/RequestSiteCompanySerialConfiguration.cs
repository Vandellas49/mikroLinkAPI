

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class RequestSiteCompanySerialConfiguration : IEntityTypeConfiguration<RequestSiteCompanySerial>
    {
        public void Configure(EntityTypeBuilder<RequestSiteCompanySerial> entity)
        {
            entity.HasOne(d => d.Cserial)
                       .WithMany(p => p.RequestSiteCompanySerial)
                       .HasForeignKey(d => d.CserialId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_RequestCompanySerial_ComponentSerial");

            entity.HasOne(d => d.Request)
                .WithMany(p => p.RequestSiteCompanySerial)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestSiteCompanySerial_Request");
        }
    }
}
