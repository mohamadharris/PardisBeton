using Microsoft.AspNetCore.Http;
using Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.About
{
    public class AboutViewModel : BaseViewModel
    {
        public int AboutId { get; set; }
        public string? Summary { get; set; }
        public string? PageContent { get; set; }
        public string? SummaryImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
