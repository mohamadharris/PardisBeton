using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.Project;

namespace PardisBeton.ViewComponents
{
    public class ProjectViewComponent : ViewComponent
    {
        private readonly BLProject _blProjects;

        public ProjectViewComponent(BLProject blProjects)
        {
             _blProjects = blProjects;
        }


        public IViewComponentResult  Invoke(string viewName = "RandomProjects", int randomProjectsCount = 9)
        {
            var randomProjects = _blProjects.GetRandomProjects(randomProjectsCount);
            var model = new ProjectCollectionViewModel { RandomProjects = randomProjects.RandomProjects };
            return View(viewName, model);
        }
    }
}
