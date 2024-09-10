using Microsoft.AspNetCore.Http;
using Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.About
{
    public class PersonnelViewModel : BaseViewModel
    {
        public int PersonnelId { get; set; }
        [Required(ErrorMessage = "لطفا نام را وارد نمایید.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید.")]
        public string Family { get; set; }
        public string? Sex { get; set; }
        public string? Nickname { get; set; }
        public string? ImagePath { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
