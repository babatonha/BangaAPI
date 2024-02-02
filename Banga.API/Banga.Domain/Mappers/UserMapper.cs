using Banga.Data.Models;
using Banga.Domain.DTOs;

namespace Banga.Domain.Mappers
{
    public static class UserMapper
    {

        public static AppUser MappUserDtoToAppUser(UserDto dto, AppUser user)
        {
            return new AppUser
            {
                UserName = dto.UserName

            };
        }

        public static AppUser MappRegisterDtoToAppUser(RegisterDto dto)
        {
           return new AppUser
            {
               UserName = dto.UserName,
               Email = dto.Email,
            };
        }
    }
}
