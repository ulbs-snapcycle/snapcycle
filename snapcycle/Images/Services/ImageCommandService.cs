using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.Images.Models;
using snapcycle.Images.Repository.Interfaces;
using snapcycle.Images.Services.Interfaces;

namespace snapcycle.Images.Services;

public class ImageCommandService : IImageCommandService
{
    private IImageRepository _repository;
    
    public ImageCommandService(IImageRepository repository)
    {
        _repository = repository;
    }

    public async Task<Image> CreateImage(String name)
    {
        Image image = await _repository.CreateAsync(name);

        return image;
    }

    public async Task DeleteImage(int id)
    {
        Image image = await _repository.GetByIdAsync(id);

        if (image == null)
        {
            throw new ItemDoesNotExist(Constants.IMAGE_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(id);
    }
}