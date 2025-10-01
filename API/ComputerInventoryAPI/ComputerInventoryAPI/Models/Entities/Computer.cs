using System.ComponentModel.DataAnnotations;

namespace ComputerInventoryAPI.Models.Entities;

public class Computer
{
    public int Id { get; set; }
    [MaxLength(25)]
    public string Name { get; set; } = "New Name";
    public string? Processor { get; set; }
    public int Ram { get; set; }
    public int Storage { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    public int OperatingSystemId { get; set; }
    public OperatingSystem? OperatingSystem { get; set; }
}