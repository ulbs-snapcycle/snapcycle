using System.ComponentModel.DataAnnotations;

namespace snapcycle.UserImages.DTOs;

public class CreateUserImageRequest
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ImageId { get; set; }
}