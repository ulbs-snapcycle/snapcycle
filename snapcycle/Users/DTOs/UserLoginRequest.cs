using System.ComponentModel.DataAnnotations;

namespace polyclinic_service.Users.DTOs;

public class UserLoginRequest
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}