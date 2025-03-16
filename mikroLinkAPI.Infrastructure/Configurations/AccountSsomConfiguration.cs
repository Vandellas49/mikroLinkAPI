using Microsoft.AspNetCore.Identity;
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
    public class AccountSsomConfiguration : IEntityTypeConfiguration<AccountSsom>
    {
        public void Configure(EntityTypeBuilder<AccountSsom> entity)
        {
            entity.ToTable("AccountSSOM");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(150)
                .IsFixedLength();

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(e => e.PhoneNumberTwo).HasMaxLength(11);

            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(90);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.AccountSsom)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountSSOM_Company");
        }
    }
}
