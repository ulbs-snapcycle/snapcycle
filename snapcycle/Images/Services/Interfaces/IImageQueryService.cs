using snapcycle.Images.Models;

namespace snapcycle.Images.Services.Interfaces;

public interface IImageQueryService
{
    Task<IEnumerable<Image>> GetAllAppointments();
    Task<Image> GetImageById(int id);
    Task<String> NewName();
}