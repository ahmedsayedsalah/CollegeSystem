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
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professors").HasKey(x => x.Id);

            builder.HasOne(x=> x.Department).WithMany(x=> x.Professors)
                .HasForeignKey(x=> x.DeptId).IsRequired();

            builder.Property(x => x.CreateTime).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
