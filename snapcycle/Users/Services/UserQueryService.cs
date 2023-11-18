using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services.Interfaces;
using snapcycle.UserImages.Models;

namespace polyclinic_service.Users.Services;

public class UserQueryService : IUserQueryService
{
    private IUserRepository _repository;
    
    public UserQueryService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        IEnumerable<User> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<User> GetUserById(int id)
    {
        User result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }

        return result;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        User user = await _repository.GetByEmailAsync(email);
        
        if (user == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }

        return user;
    }

    public async Task<User> TryLoginUser(string email, string password)
    {
        User user = await _repository.GetByEmailAsync(email);
        
        if (user == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }

        if (!user.Password.Equals(password))
        {
            throw new WrongPassword(Constants.WRONG_PASSWORD);
        }

        return user;
    }
}