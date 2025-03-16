

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(70);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasOne(d => d.Il)
                .WithMany(p => p.Company)
                .HasForeignKey(d => d.IlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_iller");
            entity.HasMany(e => e.RadevuPlanlama)
                  .WithOne(e => e.Company)
                  .HasForeignKey(e => e.CompanyId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RandevuPlanlama_Company");

        }
    }
}
