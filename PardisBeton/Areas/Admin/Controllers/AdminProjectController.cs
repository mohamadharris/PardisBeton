using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.Project;

namespace PardisBeton.Areas.Admin.Controllers
{
    public class AdminProjectController : AdminBaseController
    {
        private readonly BLProject _blProject;

        public AdminProjectController(BLProject blProject)
        {
            _blProject = blProject;
        }
        public IActionResult Index()
        {
            var projects = _blProject.GetAllProjects();
            return View(projects);
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProjectViewModel projectViewModel)
        {
            int project = _blProject.AddNewProject(projectViewModel);
            if (project > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }


        public IActionResult Edit(int id)
        {
            var projectViewModel = _blProject.GetProjectById(id);
            if (projectViewModel != null)
            {
                return View(projectViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }


        [HttpPost]
        public IActionResult Edit(ProjectViewModel projectViewModel)
        {
            var project = _blProject.UpdateProject(projectViewModel);
            if (project > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Edit), projectViewModel.ProjectId);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {

            var imageUrl = await _blProject.UploadImage(file, "projects/");
            return Json("/" + imageUrl);

        }



        public IActionResult Remove(int id)
        {
            var projectViewModel = _blProject.GetProjectById(id);
            if (projectViewModel != null)
            {

                return View(projectViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult Remove(ProjectViewModel projectViewModel)
        {
            var project = _blProject.RemoveProject(projectViewModel.ProjectId);

            if(project > 0)
            {
                return RedirectToAction(nameof(Index)); //change it to success page
            }
            else
            {
                return RedirectToAction(nameof(Index)); //Change it to error page
            }

        }
    }
}
