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

    public RefreshTokens GetByToken(string token)
    {
        return  _context.RefreshTokens.AsNoTracking().FirstOrDefault(rt => rt.RefreshToken == token);
    }
}