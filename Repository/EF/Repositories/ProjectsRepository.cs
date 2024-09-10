using DAL.dbmodel;
using Microsoft.EntityFrameworkCore;
using Repository.EF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.Repositories
{
    public class ProjectsRepository : BaseRepository<Project, int>, IProjectsRepository
    {
        public ProjectsRepository(DbContext context) : base(context)
        {
        }

       
    }
}
