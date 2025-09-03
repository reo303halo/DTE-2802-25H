using Microsoft.EntityFrameworkCore;
using StudentMVC.Data;
using StudentMVC.Models.Entities;
using StudentMVC.Models.ViewModel;

namespace StudentMVC.Models;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _db;
    private StudentEditViewModel _viewModel;

    public StudentRepository(StudentDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Student> GetAll()
    {
        var students = _db.Students
            .Include(deg => deg.Degree)
            .ToList();
        return students;
    }

    public void Save(Student student)
    {
        _db.Students.Add(student);
        _db.SaveChanges();
    }

    public StudentEditViewModel GetStudentEditViewModel()
    {
        _viewModel = new StudentEditViewModel
        {
            Degrees = _db.Degrees.ToList()
        };
        return _viewModel;
    }
}