namespace ComputerInventoryAPI.Models.Entities;

public class Brand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Computer>? Computers { get; set; }
}