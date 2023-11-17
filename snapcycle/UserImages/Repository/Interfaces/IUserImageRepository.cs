using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;

namespace snapcycle.UserImages.Repository.Interfaces;

public interface IUserImageRepository
{
    Task<IEnumerable<UserImage>> GetAllAsync();
    Task<UserImage> GetByIdAsync(int id);
    Task<UserImage> CreateAsync(CreateUserImageRequest userImageRequest);
    Task DeleteAsync(int id);
}