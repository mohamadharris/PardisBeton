using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.About;

namespace PardisBeton.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {

        private readonly BLAbout _blAbout;

        public AboutViewComponent(BLAbout blAbout)
        {
            _blAbout = blAbout;
        }

        public IViewComponentResult Invoke(string viewName, string selector)
        {
            object viewModel = null;
            switch (selector)
            {
                case "summary":
                    viewModel = _blAbout.GetAboutSummary(1);
                    break;

                case "personnel":
                    viewModel = _blAbout.GetPersonnels();
                    break;

            }

            return View(viewName, viewModel);

        }
    }
}
