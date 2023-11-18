using System.ComponentModel.DataAnnotations;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Users.DTOs;

public class UpdateScoreRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public ResultType Result { get; set; }
    
    [Required]
    public TrashType Trash { get; set; }
}