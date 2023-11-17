using snapcycle.UserImages.Models;

namespace snapcycle.UserImages.Services.Interfaces;

public interface IUserImageQueryService
{
    Task<IEnumerable<UserImage>> GetAllUserAppointments();
    Task<UserImage> GetUserAppointmentById(int id);
}