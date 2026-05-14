using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.Auth;
using MyProject.Models;
using MyProject.Services;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }
        

        // POST api/<AuthController>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest data)
        {
            Console.WriteLine($"Login attempt for email: {data.Email}");
            var user = _dbContext.User.FirstOrDefault(x => x.Email == data.Email);

            if (user == null)
            {
                Console.WriteLine($"Login failed: User with email {data.Email} not found.");
                return Unauthorized("Invalid email or password.");
            }

            if (user.Password != data.Password)
            {
                Console.WriteLine($"Login failed: Incorrect password for email {data.Email}.");
                return Unauthorized("Invalid email or password.");
            }

            var token = _authService.GenerateToken(user);
            Console.WriteLine($"Login successful for email: {data.Email}");

            return Ok(new { Message = "Login successful", Token = token,
                User = new
                {
                    Id = user.Id,
                    Name = user.FirstName + user.LastName,
                    Email = user.Email
                }
            });
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequest Signup)
        {
            Console.WriteLine($"SignUp attempt for email: {Signup?.Email}");

            if (Signup == null)
            {
                Console.WriteLine("SignUp failed: Invalid request data.");
                return BadRequest(new { Message = "Invalid request data." });
            }

            var existingUser = _dbContext.User.FirstOrDefault(x => x.Email == Signup.Email);
            if (existingUser != null)
            {
                Console.WriteLine($"SignUp failed: Email {Signup.Email} already exists.");
                return BadRequest(new { Message = "Email already exists." });
            }

            var userData = new User {
                FirstName =Signup.FirstName,
                LastName = Signup.LastName,
                Password = Signup.Password,
                Email = Signup.Email,
            };

            _dbContext.User.Add(userData);
            _dbContext.SaveChanges();

            Console.WriteLine($"User signed up successfully: {Signup.Email}");

            return Ok(new
            {
                Message = "User signed up successfully.",
                Data = new
                {
                    Id = userData.Id, 
                    Name = userData.FirstName + userData.LastName,
                    Email = userData.Email
                }
            });

        } 
    }
}
