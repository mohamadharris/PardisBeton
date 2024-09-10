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
    public class PostCategoryRepository : BaseRepository<PostCategory, int>, IPostCategoryRepository
    {
        public PostCategoryRepository(DbContext context) : base(context)
        {
        }

        
    }
}
