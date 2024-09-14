using DEPI8ASPMVCLibraryManagementSystem.Models;
using DEPI8ASPMVCLibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPI8ASPMVCLibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.UserName;
                user.PasswordHash = model.Password;
                user.Address = model.Address;
                IdentityResult result =  await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    //create cookie
                    //List<Claim> claims = new List<Claim>();
                    //claims.Add(new Claim("dressCode", "Red"));
                    //await signInManager.SignInWithClaimsAsync(user, false, claims);  //==> Id,Name,Role + Claims
                    await userManager.AddToRoleAsync(user, "Student");
                    await signInManager.SignInAsync(user,false); //==> Id,Name,Role
                    return RedirectToAction("Index", "Book");                    
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("msg", item.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(model.UserName);
                if(applicationUser != null)
                {
                    bool isFound = await userManager.CheckPasswordAsync(applicationUser, model.Password);
                    if (isFound)
                    {
                        await signInManager.SignInAsync(applicationUser, model.RememberMe);
                        return RedirectToAction("Index", "Book");
                    }                
                }
                else
                {
                    ModelState.AddModelError("msg", "username or passord is in correct");
                }
                
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.UserName;
                user.PasswordHash = model.Password;
                user.Address = model.Address;
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("msg", item.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
