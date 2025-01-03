using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class PostCategoryRepository : Repository<PostCategory>, IPostCategoryRepository
{
    private readonly BlogContext _context;
    public PostCategoryRepository(BlogContext context) : base(context)
    {
        _context = context;
    }
}