using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class RefreshTokensRepository : Repository<RefreshTokens>, IRefreshTokensRepository
{
    private readonly BlogContext _context;
    public RefreshTokensRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RefreshTokens> GetByToken(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.RefreshToken == token);
    }
    
    public RefreshTokens UpdateToken(RefreshTokens entity)
    {
        _context.RefreshTokens.Update(entity);
        return entity;
    }
}