using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using snapcycle.Images.Controllers.Interfaces;
using snapcycle.Images.Models;
using snapcycle.Images.Services.Interfaces;

namespace snapcycle.Images.Controllers;

public class ImagesController : ImagesApiController
{ 
    private IHostEnvironment _environment;
    private IImageQueryService _queryService;
    private IImageCommandService _commandService;

    private ILogger<ImagesController> _logger;
    
    public ImagesController(IImageQueryService queryService, IImageCommandService commandService, ILogger<ImagesController> logger, IHostEnvironment environment)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
        _environment = environment;
    }
    
    public override async Task<ActionResult<IEnumerable<Image>>> GetAllImages()
    {
        _logger.LogInformation("Rest request: Get all appointments.");
        try
        {
            IEnumerable<Image> result = await _queryService.GetAllAppointments();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Image>> GetImageById(int id)
    {
        _logger.LogInformation($"Rest request: Get appointment with id {id}.");
        try
        {
            Image result = await _queryService.GetImageById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Image>> CreateImage(IFormFile file)
    {
        if (!new List<String>{".png",".jpeg",".jpg",".bmp",".webp"}.Contains(Path.GetExtension(file.FileName).ToLower()))
        {
            return new BadRequestObjectResult("Only .png, .jpeg, .jpg, .webp, .bmp files are allowed.");
        }

        string extension = Path.GetExtension(file.FileName).ToLower();
        
        string name = await _queryService.NewName();
        name += extension;
        Image image = await _commandService.CreateImage(name);
        
        if (file != null! && file.Length > 0)
        {
            var storage = Path.Combine(_environment.ContentRootPath, "Images/Storage");
            
            if (!Directory.Exists(storage))
            {
                Directory.CreateDirectory(storage);
            }

            var filePath = Path.Combine(storage, image.Name );

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new OkObjectResult(image);
        }
        
        return BadRequest("Ok");
    }

    public override async Task<ActionResult> DeleteImage(int id)
    {
        _logger.LogInformation($"Rest request: Delete appointment with id {id}");
        try
        {
            Image image = await _queryService.GetImageById(id);
            var storage = Path.Combine(_environment.ContentRootPath, "Images/Storage");
            var filePath = Path.Combine(storage, image.Name);
            FileInfo file = new FileInfo(filePath);
            file.Delete();
            
            await _commandService.DeleteImage(id);

            return Accepted(Constants.IMAGE_DELETED, Constants.IMAGE_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> GetImageFileById(int id)
    {
        try
        {
            Image image = await _queryService.GetImageById(id);
            var storage = Path.Combine(_environment.ContentRootPath, "Images/Storage");
            var filePath = Path.Combine(storage, image.Name);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }
        catch (ItemDoesNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
}