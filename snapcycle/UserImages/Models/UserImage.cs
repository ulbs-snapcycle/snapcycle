using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using polyclinic_service.Users.Models;
using snapcycle.Images.Models;

namespace snapcycle.UserImages.Models;

public class UserImage
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [JsonIgnore]
    public virtual User User { get; set; }
    
    [ForeignKey("ImageId")]
    public int ImageId { get; set; }
    
    [JsonIgnore]
    public virtual Image Image { get; set; }
    
    [Required]
    public TrashType TrashType { get; set; }
}