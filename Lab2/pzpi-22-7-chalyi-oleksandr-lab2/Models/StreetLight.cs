namespace SmartLightSense.Models;

public class Streetlight
{
    public int Id { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public DateTime InstallationDate { get; set; }
    public string Status { get; set; }
    public int BrightnessLevel { get; set; }
    public DateTime LastMaintenanceDate { get; set; }

    public ICollection<Sensor>? Sensors { get; set; }
}
