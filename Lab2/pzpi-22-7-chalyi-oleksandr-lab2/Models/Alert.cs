namespace SmartLightSense.Models;

public class Alert
{
    public int Id { get; set; }
    public int? StreetlightId { get; set; } 
    public int? SensorId { get; set; } 
    public string AlertType { get; set; }
    public DateTime AlertDateTime { get; set; }
    public bool Resolved { get; set; }


    public Streetlight? Streetlight { get; set; }
    public Sensor? Sensor { get; set; }
}
