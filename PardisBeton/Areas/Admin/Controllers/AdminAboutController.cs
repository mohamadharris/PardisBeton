using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.About;

namespace PardisBeton.Areas.Admin.Controllers
{
    public class AdminAboutController : AdminBaseController
    {
        private readonly BLAbout _blAbout;

        public AdminAboutController(BLAbout blAbout)
        {
            _blAbout = blAbout;
        }

        #region About
        public IActionResult Index()
        {
            var aboutContent = _blAbout.GetAboutContent(1);

            return View(aboutContent);
        }


        public IActionResult Summary()
        {
            var aboutSummary = _blAbout.GetAboutSummary(1);

            return View(aboutSummary);
        }



        [HttpPost]
        public IActionResult EditAbout(AboutViewModel aboutViewModel)
        {
            if (aboutViewModel != null)
            {
                var about = _blAbout.UpdateAbout(aboutViewModel);
            }
            if (aboutViewModel.Summary != null)
            {
                return RedirectToAction(nameof(Summary));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {

            var imageUrl = await _blAbout.UploadImage(file, "about/");
            return Json("/" + imageUrl);

        }

        #endregion

        #region Personnel

        public IActionResult Personnel()
        {
            var personnelList = _blAbout.GetPersonnels();
            return View(personnelList);
        }


        public IActionResult AddPersonnel()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddPersonnel(PersonnelViewModel personnelViewModel)
        {
            if (personnelViewModel != null)
            {
                _blAbout.AddNewPersonnel(personnelViewModel);
            }
            return RedirectToAction(nameof(Personnel));
        }

        public IActionResult EditPersonnel(int id)
        {
            var personnelItem = _blAbout.GetPersonnelById(id);
            return View(personnelItem);
        }

        [HttpPost]
        public IActionResult EditPersonnel(PersonnelViewModel personnelViewModel)
        {
            if (personnelViewModel != null)
            {
                _blAbout.UpdatePersonnel(personnelViewModel);
            }
            return RedirectToAction(nameof(Personnel));
        }


        public IActionResult RemovePersonnel(int id)
        {
            var personnelItem = _blAbout.GetPersonnelById(id);
            if (personnelItem != null)
            {
                return View(personnelItem);
            }
            else
            {
                return RedirectToAction(nameof(Personnel));
            }

        }


        [HttpPost]
        public IActionResult RemovePersonnel(PersonnelViewModel personnelViewModel)
        {
            int personnel = _blAbout.RemovePersonnel(personnelViewModel.PersonnelId);

            return RedirectToAction(nameof(Personnel));
        }



        [HttpPost]
        public async Task<IActionResult> UploadImagePersonnel(IFormFile file)
        {

            var imageUrl = await _blAbout.UploadImage(file, "about/personnel/");
            return Json("/" + imageUrl);

        }

        #endregion
    }
}
