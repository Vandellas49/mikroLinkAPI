using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class IllerConfiguration : IEntityTypeConfiguration<Iller>
    {
        public void Configure(EntityTypeBuilder<Iller> entity)
        {
            entity.ToTable("Iller");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Sehir)
                .HasColumnName("Sehir")
                .HasMaxLength(255);
        }
    }
}
