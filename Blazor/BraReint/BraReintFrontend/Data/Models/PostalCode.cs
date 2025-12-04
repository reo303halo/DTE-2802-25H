using System.ComponentModel.DataAnnotations;

namespace BraReintFrontend.Data.Models;

public class PostalCode
{
    [Required(ErrorMessage = "Postkode krevers.")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Postkode må være nøyaktig 4 siffer.")]
    [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Postkoden kan kun inneholde tall.")]
    public string Code { get; set; } = string.Empty;
}