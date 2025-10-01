namespace ComputerInventoryAPI.Models;

public class ComputerDtoAdd
{
    public int Id { get; set; }
    public string Name { get; set; } = "Computer000";
    public string? Processor { get; set; }
    public int Ram { get; set; }
    public int Storage { get; set; }
    public int BrandId { get; set; }
    public int OperatingSystemId { get; set; }
}