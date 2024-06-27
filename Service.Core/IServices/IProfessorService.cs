using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Professor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IServices
{
    public interface IProfessorService
    {
        Task<IList<ProfessorVM>> GetAll();
        Task<IList<ProfessorVM>> Paginate(int pageIndex, int pageSize);
        Task<ProfessorVM> GetById(int id);
        Task<int> Add(ProfessorVM professor);
        Task Update(UpdateProfessorVM professor);
        Task Delete(int profId);
        Task AddCourses(int profId, IList<CourseVM> courses);
        Task<IList<CourseVM>> GetCourses(int profId);
        Task<long> GetNumberOfCourses(int profId);
    }
}
