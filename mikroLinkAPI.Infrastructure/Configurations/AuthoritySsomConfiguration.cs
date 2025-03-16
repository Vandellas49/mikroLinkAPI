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
    public class AuthoritySsomConfiguration : IEntityTypeConfiguration<AuthoritySsom>
    {
        public void Configure(EntityTypeBuilder<AuthoritySsom> entity)
        {
            entity.ToTable("AuthoritySSOM");

            entity.Property(e => e.UygulamaKodu)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.YetkiKodu)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
