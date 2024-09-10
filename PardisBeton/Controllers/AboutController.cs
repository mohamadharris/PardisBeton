using BLL;
using Microsoft.AspNetCore.Mvc;

namespace PardisBeton.Controllers
{
    public class AboutController : Controller
    {
        private readonly BLAbout _blAbout;

        public AboutController(BLAbout blAbout)
        {
            _blAbout = blAbout;
        }
        public IActionResult Index()
        {
            var about = _blAbout.GetAboutContent();
            return View(about);
        }

        public IActionResult Personnel(int id)
        {
            var personnel = _blAbout.GetPersonnelById(id);

            return View(personnel);

        }
    }
}
