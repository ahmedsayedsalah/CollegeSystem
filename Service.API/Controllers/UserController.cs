using Microsoft.AspNetCore.Mvc;
using Service.Business.Services;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Course;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IOptionsMonitor<JwtOptions> options;
        private readonly IUserService userService;

        public UserController(IOptionsMonitor<JwtOptions> options,IUserService userService)
        {
            this.options = options;
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
        [Route("register")]
        [AllowAnonymous]
        public async Task<int> Register(RegisterUserVM professor)
        {
            var usrId = await userService.Add(professor);
            return usrId;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<string> Login(AuthenticationRequest request)
        {

            // validate user
            var user = await userService.AuthenticateUser(request.Email, request.Password);

            // SecurityToken Descriptor,Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // info about token
                Issuer = options.CurrentValue.Issure,
                Audience = options.CurrentValue.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.CurrentValue.SigningKey))
                    , SecurityAlgorithms.HmacSha256),

                // info about user
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Name),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Gender, user.Gender),
                    new(ClaimTypes.MobilePhone, user.Phone),
                    new(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                    new(ClaimTypes.Role, user.Role),

                })
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
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
