using Service.Core.Models.ViewModels.Professor;
using Service.Core.Models.ViewModels;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Core.Models.ViewModels.Student;

namespace Service.Core.IServices
{
    public interface IStudentService
    {
        Task<IList<StudentVM>> GetAll();
        Task<IList<StudentVM>> Paginate(int pageIndex, int pageSize);
        Task<StudentVM> GetById(int id);
        Task<int> Add(StudentVM student);
        Task Update(UpdateStudentVM student);
        Task Delete(int stdId);
        Task EnrollCourse(int stdId, int crsId);
        Task<long> GetNumberOfEnrolledCourses(int stdId);
        Task<IList<string>> GetEnrolledCourses(int stdId);
    }
}
