using System.ComponentModel.DataAnnotations;
using StudentMVC.Models.Entities;

namespace StudentMVC.Models.ViewModel;

public class StudentEditViewModel
{
    [StringLength(8)]
    public string? StudentId { get; set; }
    [Required(ErrorMessage = "First Name Required")]
    [StringLength(35)]
    public string? Firstname { get; set; }
    [Required(ErrorMessage = "Last Name Required")]
    public string? Lastname { get; set; }
    public int DegreeId { get; set; }
    public List<Degree>? Degrees { get; set; }
}