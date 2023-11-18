using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Users.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public int Score { get; set; }
    
    [Required]
    public int CountTotal { get; set; }
    
    [Required]
    public int CountHorrible { get; set; }
    
    [Required]
    public int CountPartial { get; set; }
    
    [Required]
    public int CountPerfect { get; set; }
    
    [Required]
    public UserType Type { get; set; }
    
    public virtual List<UserImage> UserImages { get; set; }
}