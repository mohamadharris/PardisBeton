using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.ViewModels.Post;

namespace PardisBeton.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        private readonly BLPost _blPost;

        public BlogController(BLPost blPost)
        {
            _blPost = blPost;
        }
        public IActionResult Index()
        {
            var catsByCount = _blPost.CategoriesByCount();

            return View(catsByCount);
        }

        [HttpGet]
        public IActionResult GetPosts(int pageNumber, int pageSize)
        {
            var totalPosts = _blPost.GetAllPosts().Posts.Count(); // دریافت تعداد کل پست‌ها
            var posts = _blPost.GetAllPosts(pageNumber, pageSize);
           

            var totalPages = (int)Math.Ceiling((double)totalPosts / pageSize); // محاسبه تعداد کل صفحات

            var viewModel = new PostCollectionViewModel
            {
                Posts = posts.Posts,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };

            return PartialView("_BlogPostsPartial", viewModel);

        }


        public IActionResult Details(int id)
        {
            var post = _blPost.GetPostById(id);

            return View(post);
        }
    }
}
