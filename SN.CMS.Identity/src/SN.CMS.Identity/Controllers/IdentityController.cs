using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SN.CMS.Identity.Messages.Commands;
using SN.CMS.Identity.Services;
using SN.CMS.Common.Authentications.Attributes;
using System;
using SN.CMS.Common;

namespace SN.CMS.Identity.Controllers
{
    [Route("api")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityServices _identityService;

        public IdentityController(IIdentityServices identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("me")]
        [JwtAuth]
        public async Task<ApiRequestResult> Get()
        {
            var userName = await _identityService.GetAsync(UserId.ToString());
            return ApiRequestResult.Success(userName,"用户信息获取成功");
        }

        [HttpPost("sign-up")]
        public async Task<ApiRequestResult> SignUp(SignUp command)
        {
            await _identityService.SignUpAsync(command.Id, command.Name, command.Password, command.Role);
            return ApiRequestResult.Success($"用户{command.Name}注册成功");
        }

        [HttpPost("sign-in")]
        public async Task<ApiRequestResult> SignIn(SignIn command)
        {
            var result = await _identityService.SignInAsync(command.Name, command.Password);

            return ApiRequestResult.Success(result,$"用户{command.Name}登录成功");
        }

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);
    }
}

