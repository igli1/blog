using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace WebAPI.Dtos.Blog;

public class PostDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    public string? Content { get; set; }
    [Required] 
    public DateTime PublishAt { get; set; }
    [Required]
    public PostStatus Status { get; set; }
    public IEnumerable<Guid> Categories { get; set; }
}