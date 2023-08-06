using Microsoft.AspNetCore.Mvc;
using MyFirst.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MyFirst.Web.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public Account(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            { UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };





            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                //asign this user the "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {
                    //Show success notification
                    return RedirectToAction("Register");
                }
               
            }
            // show Error notification
            return View();
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new Login
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login (Login login)
        {
            var signInResult = await signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (signInResult !=null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(login.ReturnUrl))
                    {
                    return Redirect(login.ReturnUrl);
               
                
                    }
                return RedirectToAction("Index", "Home");
            }

            // show error notification
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout ()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
