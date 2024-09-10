using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels.About
{
    public class AboutCollectionViewModel : BaseViewModel
    {
        public AboutViewModel AboutItem { get; set; }

        public IEnumerable<PersonnelViewModel> Personnels { get; set; }
    }
}
