using Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PardisBeton.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AdminAreaAuthorization]
    public class AdminBaseController : Controller
    {
      
    }
}
