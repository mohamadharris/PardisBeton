using BLL;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.Post;
using Model.ViewModels.Project;

namespace PardisBeton.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        private readonly BLPost _blPost;

        public PostViewComponent(BLPost bLPost)
        {
            _blPost = bLPost;
        }


        public IViewComponentResult Invoke(string viewName = "LatestPosts", int latestPostsCount = 6)
        {
            var latestPosts = _blPost.GetAllPosts().Posts.OrderByDescending(d => d.RegDate).ThenByDescending(i => i.PostId).Take(latestPostsCount).ToList();
            var model = new PostCollectionViewModel { LatestPosts = latestPosts };
            return View(viewName, model);
        }
    }
}
