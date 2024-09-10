using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Post
{
    public class PostCollectionViewModel : BaseViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<PostViewModel> LatestPosts { get; set; }
        public List<PostCategoryViewModel> Categories { get; set; }

        public List<string> CategoriesByCount { get; set; }
    }
}
