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
    public class CompanyToCompanyAuthorizationConfiguration : IEntityTypeConfiguration<CompanyToCompanyAuthorization>
    {
        public void Configure(EntityTypeBuilder<CompanyToCompanyAuthorization> entity)
        {
            entity.HasOne(d => d.Company)
              .WithMany(p => p.CompanyToCompanyAuthorizationCompany)
              .HasForeignKey(d => d.CompanyId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CompanyToCompanyAuthorization_Company1");

            entity.HasOne(d => d.ParentCompany)
                .WithMany(p => p.CompanyToCompanyAuthorizationParentCompany)
                .HasForeignKey(d => d.ParentCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyToCompanyAuthorization_Company");
        }
    }
}
