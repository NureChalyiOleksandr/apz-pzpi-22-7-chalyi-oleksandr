namespace SmartLightSense.Models;

public class Sensor
{
    public int Id { get; set; }
    public string SensorType { get; set; }
    public DateTime InstallationDate { get; set; }
    public string Status { get; set; }
    public string Data { get; set; }
    public DateTime LastUpdate { get; set; }
    public int StreetlightId { get; set; }

    public Streetlight Streetlight { get; set; } 
}
