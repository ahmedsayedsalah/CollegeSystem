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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students").HasKey(x => x.Id);

            builder.HasOne(x => x.Department).WithMany(x => x.Students)
                .HasForeignKey(x => x.DeptId).IsRequired();

            //builder.HasMany(x => x.Courses).WithMany(x => x.Students)
            //    .UsingEntity<Enrolllment>();

            builder.Property(x => x.CreateTime).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
