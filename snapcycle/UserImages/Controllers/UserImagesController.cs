using Microsoft.AspNetCore.Mvc;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.UserImages.Controllers.Interfaces;
using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;
using snapcycle.UserImages.Services.Interfaces;

namespace snapcycle.UserImages.Controllers;

public class UserImagesController : UserImagesApiController
{
    private IUserImageQueryService _queryService;
    private IUserImageCommandService _commandService;
    private ILogger<UserImagesController> _logger;

    public UserImagesController(IUserImageQueryService queryService,
        IUserImageCommandService commandService,
        ILogger<UserImagesController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }

    public override async Task<ActionResult<IEnumerable<UserImage>>> GetAllUserAppointments()
    {
        _logger.LogInformation("Rest request: Get all user appointments.");
        try
        {
            IEnumerable<UserImage> result = await _queryService.GetAllUserAppointments();

            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<UserImage>> GetUserAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get user appointment with id {id}.");
        try
        {
            UserImage result = await _queryService.GetUserAppointmentById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<UserImage>> CreateUserImage(CreateUserImageRequest userRequest)
    {
        _logger.LogInformation($"Rest request: Create user with DTO:\n{userRequest}");
        UserImage response = await _commandService.CreateUserImage(userRequest);

        return Created(Constants.USER_CREATED, response);
    }

    public override async Task<ActionResult> DeleteUserImage(int id)
    {
        _logger.LogInformation($"Rest request: Delete user with id {id}");
        try
        {
            await _commandService.DeleteUserImage(id);

            return Accepted(Constants.USER_DELETED, Constants.USER_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}