using Microsoft.AspNetCore.Mvc;
using Service.Business.Services;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Course;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize(Policy = "UserPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IList<UserVM>> GetAll()
        {
            var users = await userService.GetAll();

            return users;
        }

        [HttpGet]
        [Route("paginate/{pageIndex}/{pageSize}")]
        public async Task<IList<UserVM>> Paginate(int pageIndex, int pageSize)
        {
            var users = await userService.Paginate(pageIndex, pageSize);
            return users;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<UserVM> GetById(int id)
        {
            var user = await userService.GetById(id);
            return user;
        }

        [HttpPost]
        [Route("add")]
        public async Task<int> Add(UserVM professor)
        {
            var usrId = await userService.Add(professor);
            return usrId;
        }

        [HttpPut]
        [Route("update")]
        public async Task Update(UpdateUserVM student)
        {
            await userService.Update(student);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task Delete(int id)
        {
            await userService.Delete(id);
        }
    }
}
