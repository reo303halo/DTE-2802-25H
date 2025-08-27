using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using StudentMVC.Models.Entities;
using StudentMVC.Models.ViewModel;

namespace StudentMVC.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository _repository;

    public StudentController(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    
    // GET
    public IActionResult Index()
    {
        var students = _repository.GetAll();
        
        return View(students);
    }
    
    // GET: Student/Create
    public IActionResult Create()
    {
        var student = _repository.GetStudentEditViewModel();
        return View(student);
    }

    [HttpPost]
    public IActionResult Create([Bind("Firstname,Lastname,DegreeId")] StudentEditViewModel student)
    {
        try
        {
            if (!ModelState.IsValid) return View();

            var random = new Random();
            var randomDigits = random.Next(0, 1000).ToString("D3");

            if (student.Firstname != null)
            {
                var studentId = $"{student.Firstname[0]}{student.Lastname?[..2]}{randomDigits}";
                student.StudentId = studentId.ToLower();
            }
            
            // Assign values from StudentEditViewModel to Student to be saved.
            var s = new Student
            {
                StudentId = student.StudentId,
                Firstname = student.Firstname,
                Lastname = student.Lastname,
                DegreeId = student.DegreeId
            };

            _repository.Save(s);
            TempData["message"] =
                $"Student {student.Lastname}, {student.Firstname} has been created with id {student.StudentId}!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View();
        }
    }
}