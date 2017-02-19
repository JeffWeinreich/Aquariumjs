using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Aquarium.Data;
using Aquarium.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aquarium.Controllers
{
    public class AuthenticationController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }

        public AuthenticationController(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpGet]
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if(User != null)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                var result = await SignInManager.PasswordSignInAsync(user, model.Password, false, true);
                
                if(result.Succeeded)
                {
                    return Redirect("~/account/user");
                }
                else if(result.IsLockedOut)
                {
                    return View("~/home");
                }
                else if(result.IsNotAllowed)
                {
                    return View("~/home");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View("~/fishes/get");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return Redirect("~/home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var user = new ApplicationUser();
            user.Email = user.UserName = model.Email;

            var result = await UserManager.CreateAsync(user, model.Password);
            await SignInManager.PasswordSignInAsync(user, model.Password, false, false);

            return View();
        }

    }
}
