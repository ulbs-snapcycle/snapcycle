using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Users.Repository.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(String email);
    Task<User> CreateAsync(CreateUserRequest userRequest);
    Task<User> UpdateAsync(UpdateUserRequest userRequest);
    Task DeleteAsync(int id);
    Task<User> UpdateScores(int id, ResultType result, TrashType trash);
}