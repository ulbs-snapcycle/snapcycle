using Microsoft.AspNetCore.Mvc;
using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;

namespace snapcycle.UserImages.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UserImagesApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<UserImage>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<UserImage>>> GetAllUserAppointments();
    
    [HttpGet("user_appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(UserImage))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserImage>> GetUserAppointmentById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(UserImage))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserImage>> CreateUserImage([FromBody]CreateUserImageRequest userImageRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteUserImage([FromRoute]int id);
}