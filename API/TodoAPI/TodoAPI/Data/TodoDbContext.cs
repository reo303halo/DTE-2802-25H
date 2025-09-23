using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

// dotnet ef migrations add <name of migration>
// dotnet ef database update

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>().ToTable("Todo");
        
        // SEEDING
        
        // TodoItems
        modelBuilder.Entity<TodoItem>().HasData(new TodoItem { Id = 1, Name = "Feed the cat", IsComplete = true });
        modelBuilder.Entity<TodoItem>().HasData(new TodoItem { Id = 2, Name = "Buy dinner", IsComplete = false });
        modelBuilder.Entity<TodoItem>().HasData(new TodoItem { Id = 3, Name = "Do the dishes", IsComplete = false });
    }
    
    public DbSet<TodoItem> TodoItems { get; set; }
}