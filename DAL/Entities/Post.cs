using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Post
{
    [Key]
    public Guid Guid { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    public string? Content { get; set; }
    [Required] 
    public DateTime CreatedAt { get; set; }
    [Required] 
    public DateTime UpdatedAt { get; set; }
    [Required] 
    public Guid UserId { get; set; }
    public User User { get; set; }
}