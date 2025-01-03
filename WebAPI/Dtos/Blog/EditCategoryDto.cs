using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Blog;

public class EditCategoryDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}