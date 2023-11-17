using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using snapcycle.UserImages.Models;

namespace snapcycle.Images.Models;

public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    public virtual List<UserImage> UserImages { get; set; }
}