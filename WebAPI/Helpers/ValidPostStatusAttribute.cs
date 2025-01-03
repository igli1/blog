using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace WebAPI.Helpers;

public class ValidPostStatusAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is PostStatus status)
        {
            if (!Enum.IsDefined(typeof(PostStatus), status))
            {
                return new ValidationResult($"Invalid post status: {status}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(PostStatus)))}");
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("Post status is required.");
    }
}