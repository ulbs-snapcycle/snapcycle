using System.Text;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.Images.Models;
using snapcycle.Images.Repository.Interfaces;
using snapcycle.Images.Services.Interfaces;

namespace snapcycle.Images.Services;

public class ImageQueryService : IImageQueryService
{
    private IImageRepository _repository;
    
    public ImageQueryService(IImageRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Image>> GetAllAppointments()
    {
        IEnumerable<Image> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.IMAGES_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<Image> GetImageById(int id)
    {
        Image result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.IMAGE_DOES_NOT_EXIST);
        }

        return result;
    }

    public async Task<String> NewName()
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        string randomString;
        do
        {
            randomString = new string(Enumerable.Range(0, 12)
                .Select(_ => characters[random.Next(characters.Length)]).ToArray());

        } while (await _repository.GetByNameAsync(randomString) != null);

        return randomString;
    }
}