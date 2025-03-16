using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class RandevuConfiguration : IEntityTypeConfiguration<Randevu>
    {
        public void Configure(EntityTypeBuilder<Randevu> entity)
        {
            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.Randevu)
                .HasForeignKey(d => d.TeamLeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Randevu_AccountSSOM");
            entity.HasOne(d => d.RandevuPlanlanma)
                .WithOne(p => p.Randevu)
                .HasForeignKey<Randevu>(c=>c.RadevuPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Randevu_RandevuPlanlama");
            entity.HasIndex(e => e.RadevuPlanId)
                 .HasDatabaseName("UX_Constraint")
                 .IsUnique();
        }
    }
}
