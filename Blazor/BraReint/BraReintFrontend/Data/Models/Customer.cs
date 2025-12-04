using System.ComponentModel.DataAnnotations;

namespace BraReintFrontend.Data.Models;

public class Customer
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string PostalCode { get; set; } = string.Empty;
    
    [Required]
    public List<int> TypeIds { get; set; } = [];
    
    [Required]
    public string StartDate { get; set; } = string.Empty;
}