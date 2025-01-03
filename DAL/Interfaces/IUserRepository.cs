using DAL.Entities;
using DAL.Models;

namespace DAL.Interfaces;

public interface IUserRepository : IRepository<User>
{
    UserRoles  GetUserWithRoles(Guid userId);
}