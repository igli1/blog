using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(BlogContext context) :base(context)
    {
        
    }
}