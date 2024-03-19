using Banga.Data.Models;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banga.Logic.Services
{

    //TODO:  
    //Forget password
    //reset password

    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async  Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                                    //.Include(p => p.Photos)
                                    .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return new UserDto();
            } 

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = UserMapper.MappRegisterDtoToAppUser(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);


            //var roleResult = await _userManager.AddToRoleAsync(user, UserType.Member.ToString());

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = registerDto.Email,
                Token = await _tokenService.CreateToken(user),
            };
        }


        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
