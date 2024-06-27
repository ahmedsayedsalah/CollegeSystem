using Framework.DataAccess.Repo;
using Microsoft.EntityFrameworkCore;
using Service.Core.IRepo;
using Service.DataAccess.Context;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess.Repo
{
    public class CourseRepository : GenericRepository<Course>,ICourseRepository
    {
        private AppDbContext dbContext;

        public CourseRepository(AppDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddStudents(Course course, IList<int> students)
        {
            course.Enrolllments = students.Select(x => new Enrolllment()
            {
                StdId= x,
                CrsId= course.Id
            }).ToList();

            await Task.CompletedTask;
        }

        public async Task<IList<Student>> GetStudents(Course course)
        {
            return await dbContext.Enrolllments
                .Where(x => x.CrsId == course.Id)
                .Select(x => x.Student).ToListAsync();
        }
    }
}
