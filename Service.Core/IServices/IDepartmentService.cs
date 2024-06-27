using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Department;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IServices
{
    public interface IDepartmentService
    {
        Task<IList<DepartmentVM>> GetAll();
        Task<DepartmentVM> GetById(int id);
        Task<int> Add(DepartmentVM department);
        Task Update(UpdateDepartmentVM department);
        Task Delete(int id);
        Task AddProfessors(int deptId, List<ProfessorVM> professors);
        Task AddCourses(int deptId, List<CourseVM> courses);
        Task AddStudents(int deptId, List<StudentVM> students);
        Task<IList<ProfessorVM>> GetProfessors(int deptId);
        Task<IList<CourseVM>> GetCourses(int deptId);
        Task<IList<StudentVM>> GetStudents(int deptId);
        Task<IList<string>> GetNamesOfCourses(int deptId);
        Task<long> GetNumberOfStudents(int deptId);
    }
}
