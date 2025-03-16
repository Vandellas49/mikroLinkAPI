using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class RandevuPlanlamaConfiguration : IEntityTypeConfiguration<RandevuPlanlanma>
    {
        public void Configure(EntityTypeBuilder<RandevuPlanlanma> entity)
        {
            entity.HasIndex(e => new { e.CompanyId, e.RandevuBaslangic, e.RandevuBitis, e.RandevuTarihi })
            .HasDatabaseName("IDX_UQ_Company_Baslangic_Bitis_Tarih")
            .IsUnique();

            entity.Property(e => e.RandevuBitis)
                .IsRequired()
            .HasMaxLength(7);

            entity.Property(e => e.RandevuTarihi).HasColumnType("date");

            entity.Property(e => e.RandevuBaslangic)
                .IsRequired()
            .HasMaxLength(7);

            entity.HasOne(d => d.Company)
                           .WithMany(p => p.RadevuPlanlama)
                           .HasForeignKey(d => d.CompanyId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK_RandevuPlanlama_Company");
        }
    }
}
