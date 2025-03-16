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
    public class IlcelerConfiguration : IEntityTypeConfiguration<Ilceler>
    {
        public void Configure(EntityTypeBuilder<Ilceler> entity)
        {
            entity.ToTable("Ilceler");
            entity.Property(e => e.Id)
                .HasColumnName("Id")
            .ValueGeneratedNever();
            entity.Property(e => e.Ilce)
                .HasColumnName("Ilce")
            .HasMaxLength(255);
            entity.Property(e => e.Sehir)
                .HasColumnName("Sehir")
                .ValueGeneratedOnAdd();
        }
    }
}
