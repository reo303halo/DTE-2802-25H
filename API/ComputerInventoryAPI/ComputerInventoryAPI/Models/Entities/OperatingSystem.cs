namespace ComputerInventoryAPI.Models.Entities;

public class OperatingSystem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Version { get; set; }
    public List<Computer>? Computers { get; set; }
}