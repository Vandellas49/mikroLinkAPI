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
    public class AccountAuthorityConfiguration : IEntityTypeConfiguration<AccountAuthority>
    {
        public void Configure(EntityTypeBuilder<AccountAuthority> entity)
        {
            entity.HasOne(d => d.Account)
                .WithMany(p => p.AccountAuthority)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountAuthority_AccountSSOM");

            entity.HasOne(d => d.Authority)
                .WithMany(p => p.AccountAuthority)
                .HasForeignKey(d => d.AuthorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountAuthority_AuthoritySSOM");

            entity.HasOne(d => d.Team)
                .WithMany(p => p.AccountAuthority)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_AccountAuthority_Teams");
        }
    }
}
