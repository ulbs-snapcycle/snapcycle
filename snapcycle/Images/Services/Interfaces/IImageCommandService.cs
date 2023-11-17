using snapcycle.Images.Models;

namespace snapcycle.Images.Services.Interfaces;

public interface IImageCommandService
{
    Task<Image> CreateImage(String name);
    Task DeleteImage(int id);
}