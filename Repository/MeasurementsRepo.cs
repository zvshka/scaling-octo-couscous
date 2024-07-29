using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Helpers;
using WebApp1.Interfaces;
using WebApp1.Models;

namespace WebApp1.Repository;

public class MeasurementsRepo : IMeasurementsRepo
{
    private readonly ApplicationDbContext _context;

    public MeasurementsRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Measurement>> GetByDeviceIdAsync(Guid deviceId, QueryObject query)
    {
        var filterDate = query.Date.Date;
        var measurements = _context.Measurements.AsQueryable()
            .Where(m => m.DeviceId == deviceId)
            .Where(m =>
                m.MeasurementDate.Ticks >= filterDate.AddMinutes(30).Ticks &&
                m.MeasurementDate.Ticks <= filterDate.AddDays(1).Ticks);

        measurements = query.IsDescending
            ? measurements.OrderByDescending(m => m.MeasurementDate)
            : measurements.OrderBy(m => m.MeasurementDate);

        return await measurements.ToListAsync();
    }
}