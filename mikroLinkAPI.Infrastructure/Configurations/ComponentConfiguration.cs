using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    internal class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> entity)
        {
            entity.Property(e => e.Id).HasMaxLength(150);

            entity.Property(e => e.EquipmentDescription)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
