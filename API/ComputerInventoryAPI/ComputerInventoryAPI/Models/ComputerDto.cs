namespace ComputerInventoryAPI.Models;

public class ComputerDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Processor { get; set; }
    public int Ram { get; set; }
    public int Storage { get; set; }
    public BrandDto? Brand { get; set; }
    public OperatingSystemDto? OperatingSystem { get; set; }
}