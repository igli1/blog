using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(BlogContext context) :base(context)
    {
        
    }
}