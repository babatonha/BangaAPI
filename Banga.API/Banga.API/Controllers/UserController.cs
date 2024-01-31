
using Banga.Data.Models;
using Banga.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersRepository;  
        public UserController(IUserService usersRepository)
        {
            _usersRepository = usersRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get() 
        {
            var users =  await _usersRepository.GetUsers();

            return Ok(users);
        }

        //dotnet ef database update --project Banga.Data --startup-project Banga.API
    }
}
