using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models;

[Table("Devices")]
public class Device
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public List<Measurement> Measurements { get; set; } = new List<Measurement>();
}