namespace BraReintFrontend.Data.Models;

public class BookingType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}