using System.ComponentModel.DataAnnotations;
using snapcycle.UserImages.Models;

namespace snapcycle.UserImages.DTOs;

public class CreateUserImageRequest
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ImageId { get; set; }
    
    [Required]
    public TrashType TrashType { get; set; }
}