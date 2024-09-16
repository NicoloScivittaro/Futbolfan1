using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FutbolFan1.Data;
using FutbolFan1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FutbolFan1.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly FutbolFanContext _context;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;

        public UserController(FutbolFanContext context, IConfiguration configuration)
        {
            _context = context;
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _key = configuration["Jwt:Key"];
        }

        // Hash the password using SHA256 (you can replace it with a more secure algorithm, e.g. BCrypt)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // API Registration Endpoint (JSON Response)
        // API Registration Endpoint (JSON Response)
        [HttpPost("api/register")]
        public async Task<IActionResult> ApiRegister([FromBody] UserRegisterModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("User already exists.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var user = new User
            {
                Email = model.Email,
                Password = HashPassword(model.Password) // Hash the password
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.Email });
        }

        // API Login Endpoint (JSON Response)
        [HttpPost("api/login")]
        public async Task<IActionResult> ApiLogin([FromBody] UserLoginModel model)
        {
            var hashedPassword = HashPassword(model.Password);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }


        // MVC Register Page (View Response)
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        // MVC Register Post (Form Submission)
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.Error = "Email is required";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                ViewBag.Error = "User Name is required";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Password is required";
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View(model);
            }

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ViewBag.Error = "User with this email already exists";
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = HashPassword(model.Password), // Hash the password
                Bio = string.Empty
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // MVC Login Page (View Response)
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        // MVC Login Post (Form Submission)
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.Error = "Email is required";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Password is required";
                return View(model);
            }

            var hashedPassword = HashPassword(model.Password);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid login credentials";
                return View(model);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            // Save the token in session or cookie
            // HttpContext.Session.SetString("JwtToken", new JwtSecurityTokenHandler().WriteToken(token));

            return RedirectToAction("Index", "Home");
        }

        // MVC Profile Page (View)
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            // Get current user based on email (assuming user email is in Claims)
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        // MVC Profile Update (Form Submission)
        [HttpPost("profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User userModel)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user fields
            user.UserName = userModel.UserName;
            user.Bio = userModel.Bio;

            // If the email is being updated, ensure it is unique
            if (userModel.Email != user.Email && await _context.Users.AnyAsync(u => u.Email == userModel.Email))
            {
                ViewBag.Error = "Email already in use";
                return View("Profile", user);
            }

            user.Email = userModel.Email;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }
    }
}
