
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class MysLislemConfiguration : IEntityTypeConfiguration<MysLislem>
    {
        public void Configure(EntityTypeBuilder<MysLislem> entity)
        {
            entity.ToTable("MYS_LISLEM");

            entity.Property(e => e.MysLdbIp)
                .IsRequired()
                .HasColumnName("MYS_LDB_IP")
            .HasMaxLength(40);

            entity.Property(e => e.MysLdbUygKodu)
                .IsRequired()
                .HasColumnName("MYS_LDB_UYG_KODU")
            .HasMaxLength(40);

            entity.Property(e => e.MysLisDname)
                .IsRequired()
                .HasColumnName("MYS_LIS_DNAME")
            .HasMaxLength(40);

            entity.Property(e => e.MysLisGuid)
                .IsRequired()
                .HasColumnName("MYS_LIS_GUID")
            .HasMaxLength(40);

            entity.Property(e => e.MysLisTstmp)
                .HasColumnName("MYS_LIS_TSTMP")
                .HasColumnType("datetime");
        }
    }
}
