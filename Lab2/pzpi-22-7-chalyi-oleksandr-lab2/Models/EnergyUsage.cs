namespace SmartLightSense.Models;

public class EnergyUsage
{
    public int Id { get; set; }
    public int StreetlightId { get; set; } 
    public DateTime Date { get; set; }
    public double EnergyConsumed { get; set; }

    public Streetlight Streetlight { get; set; }
}
