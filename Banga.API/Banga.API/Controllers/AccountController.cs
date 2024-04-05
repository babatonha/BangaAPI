using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _accountService.UserExists(registerDto.UserName))
            {
                return BadRequest("Username is taken"); 
            }

            return Ok(await _accountService.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _accountService.Login(loginDto);

            if (user.Token.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [HttpGet("forgotPassword/{username}")]
        public async Task<ActionResult<IdentityResult>> ForgotPassword(string username)
        {
            var user = await _accountService.GetCurrentUserByUsername(username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _accountService.ForgotPassword(user);

            return Ok(result);
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<IdentityResult>> ChangePassword([FromBody]ChangePasswordDTO changePasswordDTO)
        {
            var user = await _accountService.GetCurrentUserByUsername(changePasswordDTO.Username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _accountService.ChangePassword(user, changePasswordDTO.OldPassword,changePasswordDTO.NewPassword);

            return Ok(result);
        }
    }
}
