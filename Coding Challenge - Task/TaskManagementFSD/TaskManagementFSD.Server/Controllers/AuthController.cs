using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TaskManagementFSD.Server.Controllers;
using TaskManagementFSD.Server.Enums;
using TaskManagementFSD.Server.Exceptions;
using TaskManagementFSD.Server.Models;

namespace ShopSiloAppFSD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AauthController : ControllerBase
    {
        IConfiguration _config;

        private readonly TaskDBContext _context;
        public AauthController(IConfiguration configuration, TaskDBContext context)
        {
            this._config = configuration;
            _context = context;
        }

        [NonAction]
        public UserModel Validate(string identifier, string password)
        {

            UserModel s = _context.Users
                .FirstOrDefault(i => (i.Email == identifier || i.Username == identifier) && i.Password == password);

            return s;

        }

        [AllowAnonymous]
        [HttpPost("employee-login")]
        public IActionResult EmployeeLogin([FromBody] LoginRequestDto request)
        {
            IActionResult response = Unauthorized();

            var s = Validate(request.Email, request.Password);
            if (s != null)
            {

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha512Signature);

                var subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, s.Username),
                    new Claim(JwtRegisteredClaimNames.Email,s.Email),
                    new Claim(ClaimTypes.Role, s.Role.ToString()) // Assign role to the token
                    });

                var expires = DateTime.UtcNow.AddMinutes(30);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);

            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("employee-register")]
        public IActionResult CustomerRegister([FromBody] RegisterRequestDto request)
        {
            // Validate the incoming request
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("Invalid user data.");
            }

            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email || u.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict("User already exists.");
            }

            // Create a new user
            var newUser = new UserModel
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password, // No hashing implemented here
                Role = Role.Customer // Default role
            };

            // Add the new user to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(EmployeeLogin), new { email = newUser.Email }, newUser);
        }

        [AllowAnonymous]
        [HttpPost("admin-login")]
        public IActionResult AdminLogin([FromBody] LoginRequestDto request)
        {
            IActionResult response = Unauthorized();

            var s = Validate(request.Email, request.Password);
            if (s != null)
            {

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha512Signature);

                var subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, s.Username),
                    new Claim(JwtRegisteredClaimNames.Email,s.Email),
                    new Claim(ClaimTypes.Role, s.Role.ToString()) // Assign role to the token
                    });

                var expires = DateTime.UtcNow.AddMinutes(30);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);

            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("admin-register")]
        public IActionResult AdminRegister([FromBody] RegisterRequestDto request)
        {
            // Validate the incoming request
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("Invalid user data.");
            }

            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email || u.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict("User already exists.");
            }

            // Create a new user
            var newUser = new UserModel
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password, // No hashing implemented here
                Role = Role.Admin // Default role
            };

            // Add the new user to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Login), new { email = newUser.Email }, newUser);
        }
    }
}