namespace DAL.Models;

public class UserRoles
{
    public Guid UserId { get; set; }
    public Guid? RoleId { get; set; }
    public string Email { get; set; }
    public string? RoleName { get; set; }
}