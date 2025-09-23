using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentMVC.Models.Entities;

namespace StudentMVC.Data;

// From your home folder, not project folder:
// dotnet tool install --global dotnet-

// From project folder, the folder inside the solution folder:
// dotnet ef migrations add <name of migration>
// dotnet ef database update

public class StudentDbContext : IdentityDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>().ToTable("Student");
        
        // SEEDING
        
        // Degrees 
        modelBuilder.Entity<Degree>().HasData(new Degree { DegreeId = 1, Name = "Bachelor" });
        modelBuilder.Entity<Degree>().HasData(new Degree { DegreeId = 2, Name = "Master" });
        modelBuilder.Entity<Degree>().HasData(new Degree { DegreeId = 3, Name = "PhD" });
        
        // Students
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "rol000", Firstname = "Roy", Lastname = "Olsen", DegreeId = 2});
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "jbe123", Firstname = "Johnny", Lastname = "BeGood", DegreeId = 1});
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "mla789", Firstname = "Morgan", Lastname = "Larsen", DegreeId = 2});
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "lpr058", Firstname = "Linda", Lastname = "Pravdin", DegreeId = 3});
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "ksh087", Firstname = "Kelly", Lastname = "Shaddock", DegreeId = 3});
        modelBuilder.Entity<Student>().HasData(new Student { StudentId = "ono456", Firstname = "Ola", Lastname = "Normann", DegreeId = 1});
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }
    
    public DbSet<Degree>? Degrees { get; set; }
    public DbSet<Student>? Students { get; set; }
}