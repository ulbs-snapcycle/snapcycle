using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;

namespace snapcycle.UserImages.Services.Interfaces;

public interface IUserImageCommandService
{
    Task<UserImage> CreateUserImage(CreateUserImageRequest userImageRequest);
    Task DeleteUserImage(int id);
}