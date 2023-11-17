using Microsoft.AspNetCore.Mvc;
using snapcycle.Images.Models;

namespace snapcycle.Images.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ImagesApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Image>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Image>>> GetAllImages();
    
    [HttpGet("appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(Image))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Image>> GetImageById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Image))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Image>> CreateImage([FromBody]IFormFile file);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteImage([FromRoute]int id);
}