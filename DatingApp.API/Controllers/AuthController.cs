using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{

    [Route ("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
    {
		private readonly IAuthRepository _repo;
		public AuthController (IAuthRepository repo) {
			_repo = repo;
		}

		[HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate user request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExistsAsync(userForRegisterDto.Username) && userForRegisterDto.Username.Contains(" ") && (userForRegisterDto.Username.Length > 2) && userForRegisterDto.Username != "")
                return BadRequest("Username already exists");

            var userToCreate = new User{
                Username = userForRegisterDto.Username
            };

            var createUser = await _repo.RegisterAsync(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
	}
}