using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess.ConfigEntities
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrolllment>
    {
        public void Configure(EntityTypeBuilder<Enrolllment> builder)
        {
            builder.ToTable("Enrolllments").HasKey(x => new {x.StdId,x.CrsId});

            builder.HasOne(x=> x.Student)
                .WithMany(x=> x.Enrolllments)
                .HasForeignKey(x=> x.StdId)
                .IsRequired();

            builder.HasOne(x=> x.Course)
                .WithMany(x=> x.Enrolllments)
                .HasForeignKey(x=> x.CrsId)
                .IsRequired();
        }
    }
}
