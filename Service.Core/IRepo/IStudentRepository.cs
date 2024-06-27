using Framework.Core.IRepo;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IRepo
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task EnrollCourse(Student student,int crsId);
        Task<long> GetNumberOfEnrolledCourses(Student student);
        Task<IList<string>> GetEnrolledCourses(Student student);
    }
}
