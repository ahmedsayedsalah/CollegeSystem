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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses").HasKey(x => x.Id);

            builder.HasOne(x => x.Professor).WithMany(x => x.Courses)
                .HasForeignKey(x => x.ProfId).IsRequired();

            builder.HasOne(x => x.Department).WithMany(x => x.Courses)
                .HasForeignKey(x => x.DeptId).IsRequired();

            builder.Property(x=> x.CreateTime).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
