
using Banga.Data.Models;
using Banga.Domain.DTOs;
using Banga.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _usersRepository.GetUserById(id);

            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateUser(CreateUserDTO user)
        {
            await _usersRepository.UpdateUser(user);

            return Ok();
        }

        [HttpGet("AssignUserRole/{id}/{roleName}")]
        [Authorize]
        public async Task<ActionResult> AssignUserRole(int id, string roleName)
        {
            await _usersRepository.AssignUserRole(id, roleName);

            return Ok();
        }

        [HttpGet("BlockUser/{id}/{isBlocked}")]
        [Authorize]
        public async Task<ActionResult> BockUser(int id, bool isBlocked)
        {
            await _usersRepository.BlockUser(id, isBlocked);

            return Ok();
        }

    }
}
