namespace SmartLightSense.Models;

public class MaintenanceLog
{
    public int Id { get; set; }
    public int? StreetlightId { get; set; }
    public int? AlertId { get; set; }
    public int TechnicianId { get; set; } 
    public DateTime Date { get; set; }
    public string IssueReported { get; set; }
    public string ActionTaken { get; set; }
    public string Status { get; set; }


    public Streetlight? Streetlight { get; set; }
    public User? Technician { get; set; }
    public Alert? Alert { get; set; }
}
