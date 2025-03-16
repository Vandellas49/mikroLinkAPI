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
    public class FirmaMalzemeBarcodeConfiguration : IEntityTypeConfiguration<FirmaMalzemeBarcode>
    {
        public void Configure(EntityTypeBuilder<FirmaMalzemeBarcode> entity)
        {
            entity.HasNoKey();
            entity.Property(e => e.Barcode)
                .IsRequired()
                .HasMaxLength(5000)
                .IsFixedLength();
        }
    }
}
