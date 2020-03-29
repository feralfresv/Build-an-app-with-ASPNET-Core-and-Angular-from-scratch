

using System.Threading.Tasks;
using Dating.API.Data;
using Dating.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dating.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            //validate the request
            username = username.ToLower();

            if(await _repo.UserExists(username))
                return BadRequest("Username already exists");

            var userToCreat = new User
            {
                Username = username
            };

            var createdUser = await _repo.Register(userToCreat, password);

            return StatusCode(201);

        }
    }
}