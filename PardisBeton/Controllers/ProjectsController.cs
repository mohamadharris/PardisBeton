using BLL;
using Microsoft.AspNetCore.Mvc;

namespace PardisBeton.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly BLProject _blProject;


        public ProjectsController(BLProject blProject)
        {
            _blProject = blProject;
        }
        public IActionResult Index()
        {
            var projects = _blProject.GetAllProjects(0, 12);

            return View(projects);
        }

        public IActionResult Details(int id, string? name)
        {
            var projectViewModel = _blProject.GetProjectById(id);

            return View(projectViewModel);
        }

        [HttpGet]
        public IActionResult LoadMoreProjects(int skip, int take = 15)
        {
            var projects = _blProject.GetAllProjects(skip, take);
            if (projects == null || projects.Projects.Count() == 0  )
            {
                return Json(new { success = false});
            }
            
            return PartialView("_ProjectListPartial", projects);
        }
    }
}
