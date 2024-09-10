using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Common.Attributes;

namespace PardisBeton.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public DashboardController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login", new { area = "Admin" });
        }
    }
}