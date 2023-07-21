using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NepaliBazar.Models;

namespace NepaliBazarWeb.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> singInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = singInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            IdentityUser iUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.Phone,

            };
            IdentityResult  res = await _userManager.CreateAsync(iUser,user.Password);
            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(iUser, user.Role);
                return Redirect("/");
            }
            return View(User);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IdentityUser u = await _userManager.FindByEmailAsync(model.Email);
            if (u != null)
            {
                bool res = await _userManager.CheckPasswordAsync(u, model.Password);
                if (res)
                {
                    await _signInManager.SignInAsync(u, true);
                    return Redirect("/");
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
