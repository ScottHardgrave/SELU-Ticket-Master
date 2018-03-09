using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Web.Areas.Api.Models.Users;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    [Route("api/users")]
    public class UsersApiController : Controller
    {
        private readonly UserService userService;

        public UsersApiController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody]UserLoginDto loginDto)
        {
            var result = await userService.Login(loginDto);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }
            var user = result.Data;
            await CreateAuthenticationTicket(user, loginDto.RemeberMe);
            return Ok(user);
        }

        [HttpGet("logout")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto registerDto)
        {
            var result = await userService.Register(registerDto);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPut("billing-info")]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBillingInfo([FromBody]Address address)
        {
            var user = User;
            var result = await userService.UpdateAddress(user, address);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        private async Task CreateAuthenticationTicket(UserDto user, bool persistant)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = persistant
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}