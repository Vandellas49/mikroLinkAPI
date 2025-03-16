

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class RequestedMaterialConfiguration : IEntityTypeConfiguration<RequestedMaterial>
    {
        public void Configure(EntityTypeBuilder<RequestedMaterial> entity)
        {
            entity.Property(e => e.ComponentId)
                .IsRequired()
            .HasMaxLength(150);

            entity.HasOne(d => d.Component)
                .WithMany(p => p.RequestedMaterial)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestedMaterial_Component");

            entity.HasOne(d => d.Request)
                .WithMany(p => p.RequestedMaterial)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestedMaterial_Request");
        }
    }
}
