using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;
using snapcycle.UserImages.Repository.Interfaces;
using snapcycle.UserImages.Services.Interfaces;

namespace snapcycle.UserImages.Services;

public class UserImageCommandService : IUserImageCommandService
{
    private IUserImageRepository _repository;
    
    public UserImageCommandService(IUserImageRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserImage> CreateUserImage(CreateUserImageRequest userImageRequest)
    {
        UserImage userImage = await _repository.CreateAsync(userImageRequest);

        return userImage;
    }

    public async Task DeleteUserImage(int id)
    {
        UserImage userImage = await _repository.GetByIdAsync(id);

        if (userImage == null)
        {
            throw new ItemDoesNotExist(Constants.IMAGE_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(id);
    }
}