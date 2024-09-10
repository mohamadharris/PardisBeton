using DAL.dbmodel;
using Microsoft.EntityFrameworkCore;
using Repository.EF.Base;

namespace Repository.EF.Repositories
{
    public class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetPostsByCategoryID(int categoryID)
        {
           return DbSet.Where(p => p.PostCategoryId == categoryID).ToList();
        }
    }
}
