using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Post
{
    public class PostCategoryViewModel : BaseViewModel
    {
        public int PostCategoryId { get; set; }

        public string? Name { get; set; }

        public bool? Active { get; set; }

        public virtual ICollection<PostViewModel> Posts { get; set; }
    }
}
