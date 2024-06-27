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
    public class ProfessorRepository : GenericRepository<Professor>, IProfessorsRepository
    {
        private AppDbContext dbContext;

        public ProfessorRepository(AppDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task AddCourses(Professor professor, IList<Course> courses)
        {
            professor.Courses.AddRange(courses);

            await Task.CompletedTask;
        }

        public async Task<IList<Course>> GetCourses(int profId)
        {
            var query= await dbContext
                .Professors.Include(x=> x.Courses).
                FirstOrDefaultAsync(x=> x.Id == profId);

            return query.Courses.ToList();
        }

        public async Task<long> GetNumberOfCourses(int profId)
        {
            var query = await dbContext
                .Professors.Include(x => x.Courses).
                FirstOrDefaultAsync(x => x.Id == profId);

            return query.Courses.LongCount();
        }
    }
}
