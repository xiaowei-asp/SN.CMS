using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SN.CMS.Identity.Messages.Commands;
using SN.CMS.Identity.Services;

namespace SN.CMS.Identity.Controllers
{
    [Route("")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityServices _identityService;

        public IdentityController(IIdentityServices identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await _identityService.SignUpAsync(command.Id, command.Name, command.Password, command.Role);
            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            var result = await _identityService.SignInAsync(command.Name, command.Password);
            return Ok(result);
        }
    }
}

