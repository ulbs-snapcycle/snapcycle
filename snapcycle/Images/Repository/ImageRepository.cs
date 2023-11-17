using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using snapcycle.Images.Models;
using snapcycle.Images.Repository.Interfaces;

namespace snapcycle.Images.Repository;

public class ImageRepository : IImageRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public ImageRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _context.Images
            .Include(appointment => appointment.UserImages)
            .ToListAsync();
    }
    
    public async Task<Image> GetByIdAsync(int id)
    {
        return await _context.Images.FirstOrDefaultAsync(appointment => appointment.Id == id);
    }

    public async Task<Image> GetByNameAsync(String name)
    {
        return await _context.Images.FirstOrDefaultAsync(image => image.Name.Equals(name));
    }

    public async Task<Image> CreateAsync(String name)
    {
        Image image = new Image();
        image.Name = name;
        
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
        
        return image;
    }

    public async Task DeleteAsync(int id)
    {
        Image image = await _context.Images.FindAsync(id);
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
    }
}