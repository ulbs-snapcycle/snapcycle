using snapcycle.Images.Models;

namespace snapcycle.Images.Repository.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllAsync();
    Task<Image> GetByIdAsync(int id);
    Task<Image> GetByNameAsync(String name);
    Task<Image> CreateAsync(String name);
    Task DeleteAsync(int id);
}