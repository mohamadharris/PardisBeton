using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Contact
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "لطفاً نامتان را وارد نمایید")]
        [StringLength(100, ErrorMessage = "نام وارد شده طولانی است")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفاً ایمیل را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }

       
        public string? Subject { get; set; }

        [Required(ErrorMessage = "لطفاً پیامتان را وارد نمایید")]
        [StringLength(500, ErrorMessage = "حاکثر کاراکتر مجاز 500 عدد می باشد")]
        public string Message { get; set; }
    }
}
