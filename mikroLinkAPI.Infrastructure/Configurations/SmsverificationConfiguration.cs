using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mikroLinkAPI.Domain.Abstractions;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Infrastructure.Configurations
{
    public class SmsverificationConfiguration : IEntityTypeConfiguration<Smsverification>
    {
        public void Configure(EntityTypeBuilder<Smsverification> entity)
        {
            entity.ToTable("SMSVerification");

            entity.Property(e => e.ApplicationCode)
                .IsRequired()
            .HasMaxLength(100);

            entity.Property(e => e.CurrentDate).HasColumnType("datetime");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
