using StudentMVC.Models.Entities;
using StudentMVC.Models.ViewModel;

namespace StudentMVC.Models;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();

    void Save(Student student);

    StudentEditViewModel GetStudentEditViewModel();
}