namespace GarageMVC.Models.Entities;

public class Car
{
    public string LicenseNumber { get; set; } = "No license";
    public string Make { get; set; } = "Unknown";
    public string Model { get; set; } = "Unknown";
    public int Year { get; set; }
}