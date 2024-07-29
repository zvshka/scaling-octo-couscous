using WebApp1.Helpers;
using WebApp1.Models;

namespace WebApp1.Interfaces;

public interface IMeasurementsRepo
{
    Task<List<Measurement>> GetByDeviceIdAsync(Guid deviceId, QueryObject query);
}