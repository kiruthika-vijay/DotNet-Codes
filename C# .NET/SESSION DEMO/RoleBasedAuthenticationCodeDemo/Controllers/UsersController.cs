using RoleBasedAuthenticationCodeDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthenticationCodeDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Customer")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Admin, Customer")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user, [FromQuery] List<int> roleIds)
        {
            try
            {
                var createdUser = await _userRepository.AddUser(user, roleIds);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user, [FromQuery] List<int> roleIds)
        {
            if (id != user.Id)
            {
                return BadRequest("ID mismatch in the URL and body.");
            }
            try
            {
                await _userRepository.UpdateUser(user, roleIds);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found.")
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found.")
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}
