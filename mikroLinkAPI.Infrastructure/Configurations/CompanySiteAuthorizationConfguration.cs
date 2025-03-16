using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class CompanySiteAuthorizationConfguration : IEntityTypeConfiguration<CompanySiteAuthorization>
    {
        public void Configure(EntityTypeBuilder<CompanySiteAuthorization> entity)
        {
            entity.HasOne(d => d.Company)
                     .WithMany(p => p.CompanySiteAuthorization)
                     .HasForeignKey(d => d.CompanyId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_CompanySiteAuthorization_Company");

            entity.HasOne(d => d.Site)
                .WithMany(p => p.CompanySiteAuthorization)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanySiteAuthorization_Site");
        }
    }
}
