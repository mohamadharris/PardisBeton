using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels.Post
{
    public class PostViewModel : BaseViewModel
    {
        public int PostId { get; set; }

        public int? PostCategoryId { get; set; }

        [Required(ErrorMessage ="عنوان پست وارد نشده است")]
        public string Title { get; set; } = null!;

        public DateTime RegDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Summary { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public bool? Active { get; set; }


        public string PostCategoryName { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}
