using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.DTO.Devices;
using WebApp1.Helpers;
using WebApp1.Interfaces;
using WebApp1.Models;

namespace WebApp1.Repository;

public class DevicesRepo : IDevicesRepo
{
    private readonly ApplicationDbContext _context;

    public DevicesRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Device?> GetByIdAsync(Guid id)
    {
        return await _context.Devices.FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<Device> CreateAsync(CreateDeviceDto deviceModel)
    {
        var device = new Device
        {
            Id = deviceModel.Id,
            Name = deviceModel.Name
        };
        
        await _context.Devices.AddAsync(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task<Device?> DeleteAsync(Guid id)
    {
        var deviceModel = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
        if (deviceModel == null)
        {
            return null;
        }

        _context.Devices.Remove(deviceModel);
        await _context.SaveChangesAsync();
        return deviceModel;
    }

    public async Task<List<Device>> GetAllAsync(QueryObject query)
    {
        var devices = _context.Devices.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            devices = devices.Where(s => s.Name.Contains(query.Search));
        }
        
        return await devices.ToListAsync();
    }

    public Task<bool> DeviceExists(Guid id)
    {
        return _context.Devices.AnyAsync(s => s.Id == id);
    }

    public async Task<Device?> UpdateAsync(Guid id, UpdateDeviceRequestDto deviceDto)
    {
        var existingDevice = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
        if (existingDevice == null)
        {
            return null;
        }

        existingDevice.Name = deviceDto.Name;

        await _context.SaveChangesAsync();
        return existingDevice;
    }
}