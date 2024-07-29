using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Packaging.Signing;

namespace WebApp1.Models;

[Table("Measurements")]
public class Measurement
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }

    public DateTime MeasurementDate { get; set; }

    public double ActiveInput { get; set; }
    public double ActiveOutput { get; set; }
    public double ReactiveInput { get; set; }
    public double ReactiveOutput { get; set; }
}