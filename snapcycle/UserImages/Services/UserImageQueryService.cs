using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.UserImages.Models;
using snapcycle.UserImages.Repository.Interfaces;
using snapcycle.UserImages.Services.Interfaces;

namespace snapcycle.UserImages.Services;

public class UserImageQueryService : IUserImageQueryService
{
    private IUserImageRepository _repository;
    
    public UserImageQueryService(IUserImageRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<UserImage>> GetAllUserAppointments()
    {
        IEnumerable<UserImage> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.USER_IMAGES_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<UserImage> GetUserAppointmentById(int id)
    {
        UserImage result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.USER_IMAGE_DOES_NOT_EXIST);
        }

        return result;
    }
}