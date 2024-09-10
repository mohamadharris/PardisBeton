using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.Post;
using Model.ViewModels.Project;

namespace PardisBeton.Areas.Admin.Controllers
{
    public class AdminPostController : AdminBaseController
    {
        private readonly BLPost _blPost;
        public AdminPostController(BLPost blPost)
        {
            _blPost = blPost;
        }

        public IActionResult Index()
        {
            var postList = _blPost.GetAllPosts();
            return View(postList);
        }


        public IActionResult Create()
        {
            var postViewModel = _blPost.SelectListCatgeories();
            return View(postViewModel);
        }


        [HttpPost]
        public IActionResult Create(PostViewModel postViewModel)
        {
            _blPost.AddNewPost(postViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var postViewModel= _blPost.GetPostById(id);

            return View(postViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PostViewModel postViewModel) 
        {
            var post = _blPost.UpdatePost(postViewModel);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {

            var imageUrl = await _blPost.UploadImage(file, "posts/");
            return Json("/" + imageUrl);

        }



        public IActionResult Remove(int id)
        {
            var postViewModel = _blPost.GetPostById(id);
            if (postViewModel != null)
            {

                return View(postViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult Remove(PostViewModel postViewModel)
        {
            var post = _blPost.RemovePost(postViewModel.PostId);

            if (post > 0)
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
