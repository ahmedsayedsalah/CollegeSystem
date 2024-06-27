using Framework.Core.IRepo;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IRepo
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task AddStudents(Course course, IList<int> students);
        Task<IList<Student>> GetStudents(Course course);
    }
}
