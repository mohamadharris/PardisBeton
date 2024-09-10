using Microsoft.AspNetCore.Http;
using Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Project
{
    public class ProjectViewModel : BaseViewModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "لطفاً نام پروژه را وارد نمایید.")]
        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public string? ImagePath { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Summary { get; set; }

        [DisplayFormat(HtmlEncode = true)]
        public string? Description { get; set; }

        public string? Client { get; set; }

        public DateTime? RegDate { get; set; }

        public bool? Active { get; set; }

        public bool? ShowAtHome { get; set; }

        public int? Priority { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

  
}
