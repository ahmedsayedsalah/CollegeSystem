using Framework.Core.IRepo;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IRepo
{
    public interface IProfessorsRepository : IGenericRepository<Professor>
    {
        Task AddCourses(Professor professor,IList<Course> courses);
        Task<IList<Course>> GetCourses(int profId);
        Task<long> GetNumberOfCourses(int profId);
    }
}
