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
    public class AboutRepository : BaseRepository<About, int>, IAboutRepository
    {
        public AboutRepository(DbContext context) : base(context)
        {
        }
    }
}
