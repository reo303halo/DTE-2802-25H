namespace StudentMVC.Models.Entities;

public class Degree
{
    public int DegreeId { get; set; } 
    public string Name { get; set; } // Example: 1: Bachlor, 2: Master, 3: Phd
    public List<Student>? Students { get; set; }
}