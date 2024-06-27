using Service.Core.Models.ViewModels.Student;
using Service.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Core.Models.ViewModels.Course;
using Service.Entities.Entity;

namespace Service.Core.IServices
{
    public interface ICourseService
    {
        Task<IList<CourseVM>> GetAll();
        Task<IList<CourseVM>> Paginate(int pageIndex, int pageSize);
        Task<CourseVM> GetById(int id);
        Task<int> Add(CourseVM course);
        Task Update(UpdateCourseVM course);
        Task Delete(int crsId);

        // By Amins
        Task AddStudents(int crsId, IList<int> students);
        Task<IList<StudentVM>> GetStudents(int crsId);
    }
}
