using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Category
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}