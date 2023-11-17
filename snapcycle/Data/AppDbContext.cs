using Microsoft.EntityFrameworkCore;
using polyclinic_service.Users.Models;
using snapcycle.Images.Models;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Image> Images { get; set; }
    
    public virtual DbSet<UserImage> UserImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserImage>()
            .HasOne(userImage => userImage.User)
            .WithMany(user => user.UserImages)
            .HasForeignKey(userImage => userImage.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserImage>()
            .HasOne(userImage => userImage.Image)
            .WithMany(image => image.UserImages)
            .HasForeignKey(userImage => userImage.ImageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}