using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Users.Services.Interfaces;

public interface IUserCommandService
{
    Task<User> CreateUser(CreateUserRequest userRequest);
    Task<User> UpdateUser(UpdateUserRequest userRequest);
    Task DeleteUser(int id);
    Task<User> UpdateScore(int id, ResultType result, TrashType trash);
}