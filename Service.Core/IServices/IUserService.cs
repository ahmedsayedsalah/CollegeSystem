using Service.Core.Models.ViewModels.Course;
using Service.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Core.Models.ViewModels.User;

namespace Service.Core.IServices
{
    public interface IUserService
    {
        Task<IList<UserVM>> GetAll();
        Task<IList<UserVM>> Paginate(int pageIndex, int pageSize);
        Task<UserVM> GetById(int id);
        Task<int> Add(RegisterUserVM user);
        Task Update(UpdateUserVM user);
        Task Delete(int usrId);
        public Task<UserVM> AuthenticateUser(string email, string password);
    }
}
