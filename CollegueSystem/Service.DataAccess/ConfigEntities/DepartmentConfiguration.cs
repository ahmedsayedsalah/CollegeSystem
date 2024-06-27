using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess.ConfigEntities
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments").HasKey(x => x.Id);

            builder.Property(x => x.CreateTime).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
