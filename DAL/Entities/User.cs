using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;


public class User : IdentityUser<Guid>
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    public ICollection<RefreshTokens>? RefreshTokens { get; set; }
}