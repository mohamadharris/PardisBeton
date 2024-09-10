using DAL.dbmodel;
using Repository.EF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.Repositories
{
    public interface IProjectsRepository : IBaseRepository<Project,int>
    {
   
    }
}
