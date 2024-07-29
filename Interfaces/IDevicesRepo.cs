using WebApp1.DTO.Devices;
using WebApp1.Helpers;
using WebApp1.Models;

namespace WebApp1.Interfaces;

public interface IDevicesRepo
{
    Task<Device?> GetByIdAsync(Guid id);
    Task<Device> CreateAsync(CreateDeviceDto deviceDto);
    Task<Device?> DeleteAsync(Guid id);
    Task<List<Device>> GetAllAsync(QueryObject query);
    Task<bool> DeviceExists(Guid id);
    Task<Device?> UpdateAsync(Guid id, UpdateDeviceRequestDto deviceDto);
}