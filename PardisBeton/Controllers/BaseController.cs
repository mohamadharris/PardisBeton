using Microsoft.AspNetCore.Mvc;

namespace PardisBeton.Controllers
{
    public class BaseController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
