using Framework.DataAccess.Repo;
using Service.Entities.Entity;
using Service.Core.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Service.DataAccess.Repo
{
    public class DepartmentRepository : GenericRepository<Department>,
        IDepartmentRepository
    {
        private AppDbContext dbContext;

        public DepartmentRepository(AppDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddProfessors(Department department, List<Professor> professors)
        {
            //foreach(var professor in professors)
            //    department.Professors.Add(professor);

            department.Professors.AddRange(professors);

            await Task.CompletedTask;
        }

        public async Task AddCourses(Department department, List<Course> courses)
        {
            department.Courses.AddRange(courses);

            await Task.CompletedTask;
        }

        public async Task AddStudents(Department department, List<Student> students)
        {
            department.Students.AddRange(students);

            await Task.CompletedTask;
        }

        public async Task<IList<Professor>> GetProfessors(int deptId)
        {
            var query = await dbContext.Departments.Include(x => x.Professors).FirstOrDefaultAsync(x => x.Id == deptId);

            return query.Professors.ToList();
        }

        public async Task<IList<Course>> GetCourses(int deptId)
        {
            var query= await dbContext
                .Departments.Include(x=> x.Courses)
                .FirstOrDefaultAsync(x=> x.Id == deptId);

            return  query.Courses.ToList();
        }

        public async Task<IList<Student>> GetStudents(int deptId)
        {
            var query = await dbContext
                .Departments.Include(x=> x.Students)
                .FirstOrDefaultAsync(x => x.Id == deptId);

            return query.Students.ToList();
        }

        public async Task<IList<string>> GetNamesOfCourses(int deptId)
        {
            var query= await dbContext
                .Departments.Include(x=> x.Courses)
                .FirstOrDefaultAsync(x=> x.Id==deptId);

            return query.Courses.Select(x=> x.Name).ToList();
        }

        public async Task<long> GetNumberOfStudents(int deptId)
        {
            var query= await dbContext
                .Departments.Include(x=> x.Students)
                .FirstOrDefaultAsync(x=> x.Id==deptId);

            return query.Students.LongCount();
        }
    }
}
