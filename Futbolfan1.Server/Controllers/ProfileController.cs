using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FutbolFan1.Models;
using System.Threading.Tasks;

namespace FutbolFan1.Controllers
{
    [Route("user")]
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /user/profile
        [HttpGet("Profile")]
        public async Task<IActionResult> ProfileView()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserProfileModel
            {
                Email = user.Email,
                UserName = user.UserName,
                Bio = string.Empty // Retrieve bio if it's stored in your database
            };

            return View(model);
        }

        // POST: /user/profile
        [HttpPost("Profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserProfileModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            // Add logic here to update the bio if it's stored in the database

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ProfileView");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
