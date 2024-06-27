using Framework.Core.IRepo;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IRepo
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task AddProfessors(Department department, List<Professor> professors);
        Task AddCourses(Department department, List<Course> courses);
        Task AddStudents(Department department, List<Student> students);
        Task<IList<Professor>> GetProfessors(int deptId);
        Task<IList<Course>> GetCourses(int deptId);
        Task<IList<Student>> GetStudents(int deptId);
        Task<IList<string>> GetNamesOfCourses(int deptId);
        Task<long> GetNumberOfStudents(int deptId);
    }
}
