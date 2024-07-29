using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<Measurement> Measurements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Measurement>()
            .HasOne(m => m.Device)
            .WithMany(m => m.Measurements)
            .HasForeignKey(m => m.DeviceId);

        //Наполняем таблицу счетчиков
        var devices = new List<Device>();
        for (var i = 0; i < 10; i += 1)
        {
            devices.Add(new Device
            {
                Id = Guid.NewGuid(),
                Name = $"Счетчик {i + 1}"
            });
        }

        builder.Entity<Device>().HasData(devices);

        //Наполняем таблицу измерений случайными данными
        var rnd = new Random();
        var startDate = new DateTime(2024, 07, 29);
        var currentDate = startDate;
        for (var i = 31; i > 15; i -= 1)
        {
            var measurements = new List<Measurement>();
            for (var j = 0; j < 48; j += 1)
            {
                currentDate = currentDate.AddMinutes(-30);
                measurements.AddRange(devices.Select(device => new Measurement
                {
                    Id = Guid.NewGuid(),
                    DeviceId = device.Id,
                    MeasurementDate = currentDate,
                    ActiveInput = rnd.NextDouble() * 1000,
                    ActiveOutput = rnd.NextDouble() * 1000,
                    ReactiveInput = rnd.NextDouble() * 1000,
                    ReactiveOutput = rnd.NextDouble() * 1000,
                }));
            }
            builder.Entity<Measurement>().HasData(measurements);
        }
        //Добавляем роли пользователей
        var roles = new List<IdentityRole>
        {
            new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new()
            {
                Name = "User",
                NormalizedName = "USER"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}