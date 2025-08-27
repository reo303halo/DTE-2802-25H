using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models.Entities;

public class Car
{
    [MaxLength(10)]
    public string CarId { get; set; } = "No license number";
    public string Make { get; set; } = "Unknown";
    public string Model { get; set; } = "Unknown";
    public int Year { get; set; }
}