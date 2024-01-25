
using Banga.Data.Models;
using Banga.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersRepository;  
        public UsersController(IUsersService usersRepository)
        {
            _usersRepository = usersRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get() 
        {
            var users =  await _usersRepository.GetUsers();

            return Ok(users);
        }

        //dotnet ef database update --project Banga.Data --startup-project Banga.API
    }
}
