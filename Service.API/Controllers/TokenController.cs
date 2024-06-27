using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Core.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TokenController : ControllerBase
    {
        private IOptionsMonitor<JwtOptions> options;
        private IUserService userService;

        public TokenController(IOptionsMonitor<JwtOptions> options,IUserService userService)
        {
            this.options = options;
            this.userService = userService;
        }

        [HttpPost]
        [Route("GenerateToken")]
        public async Task<string> GenerateToken(AuthenticationRequest request)
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
            var accessToken= tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}
