using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.Project
{
    public class ProjectCollectionViewModel : BaseViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public IEnumerable<ProjectViewModel> RandomProjects { get; set; }
            
    }

   
}
