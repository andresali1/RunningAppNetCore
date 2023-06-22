using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningAppNetCore.Data;
using RunningAppNetCore.Models;
using RunningAppNetCore.ViewModels;

namespace RunningAppNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
    }

        //Get: Login
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if(user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }

                //Password is incorrect
                TempData["Error"] = "Credenciales incorrectas, Por favor, intenetelo nuevamente";
                return View(loginVM);
            }

            //User was not Found
            TempData["Error"] = "Credenciales incorrectas, Por favor, intenetelo nuevamente";
            return View(loginVM);
        }
    }
}
