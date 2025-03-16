using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class ComponentSerialConfiguration : IEntityTypeConfiguration<ComponentSerial>
    {
        public void Configure(EntityTypeBuilder<ComponentSerial> entity)
        {
            entity.ToTable(p => p.HasTrigger("triggerComponentSerialAdd"));
            entity.HasIndex(e => e.SeriNo)
                        .HasDatabaseName("UX_Constraint")
                        .IsUnique().HasFilter("[SeriNo] <> 'Sarfmalzeme'");
            entity.Property(e => e.ComponentId)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.GIrsaliyeNo)
                .IsRequired()
                .HasColumnName("G_IrsaliyeNo")
                .HasMaxLength(150);

            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Shelf)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.HasOne(d => d.Company)
                .WithMany(p => p.ComponentSerial)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_ComponentSerial_Company");

            entity.HasOne(d => d.Component)
                .WithMany(p => p.ComponentSerial)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComponentSerial_Component");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.ComponentSerial)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_ComponentSerial_Site");

            entity.HasOne(d => d.TeamLeader)
                .WithMany(p => p.ComponentSerialTeamLeader)
                .HasForeignKey(d => d.TeamLeaderId)
                .HasConstraintName("FK_ComponentSerial_AccountSSOM");

            entity.HasOne(d => d.WhoDone)
                .WithMany(p => p.ComponentSerialWhoDone)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ComponentSerial_AccountSSOM1");
        }
    }
}
