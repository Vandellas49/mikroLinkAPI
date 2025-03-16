using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    internal class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> entity)
        {
            entity.Property(e => e.KordinatE)
                           .IsRequired()
            .HasMaxLength(90);

            entity.Property(e => e.KordinatN)
                .IsRequired()
            .HasMaxLength(90);

            entity.Property(e => e.PlanId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(90);

            entity.Property(e => e.SiteId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.SiteName)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.SiteTip)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Il)
                .WithMany(p => p.Site)
                .HasForeignKey(d => d.IlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Site_iller");
        }
    }
}
