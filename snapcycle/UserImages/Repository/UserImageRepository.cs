using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using snapcycle.UserImages.DTOs;
using snapcycle.UserImages.Models;
using snapcycle.UserImages.Repository.Interfaces;

namespace snapcycle.UserImages.Repository;

public class UserImageRepository : IUserImageRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public UserImageRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserImage>> GetAllAsync()
    {
        return await _context.UserImages
            .Include(userAppointment => userAppointment.User)
            .Include(userAppointment => userAppointment.Image)
            .ToListAsync();
    }

    public async Task<UserImage> GetByIdAsync(int id)
    {
        return (await _context.UserImages
            .Include(userAppointment => userAppointment.User)
            .Include(userAppointment => userAppointment.Image)
            .FirstOrDefaultAsync(userAppointment => userAppointment.Id == id))!;
    }

    public async Task<UserImage> CreateAsync(CreateUserImageRequest userImageRequest)
    {
        UserImage userImage = _mapper.Map<UserImage>(userImageRequest);
        _context.UserImages.Add(userImage);
        await _context.SaveChangesAsync();
        return userImage;
    }
    
    public async Task DeleteAsync(int id)
    {
        UserImage userImage = await _context.UserImages.FindAsync(id);
        _context.UserImages.Remove(userImage);
        await _context.SaveChangesAsync();
    }
}