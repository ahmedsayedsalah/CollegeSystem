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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private AppDbContext dbContext;

        public StudentRepository(AppDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task EnrollCourse(Student student, int crsId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == crsId);

            student.Enrolllments = new List<Enrolllment>()
            {
                new Enrolllment()
                {
                    Student = student,
                    Course = course
                    //StdId = student.Id,
                    //CrsId = crsId
                }
            };
        }

        public async Task<long> GetNumberOfEnrolledCourses(Student student)
        {
            return await dbContext
                .Enrolllments.LongCountAsync(x => x.StdId == student.Id);
        }

        public async Task<IList<string>> GetEnrolledCourses(Student student)
        {
            return await dbContext
                .Enrolllments.Where(x => x.StdId == student.Id)
                .Select(x => x.Course.Name).ToListAsync();
        }
    }
}
