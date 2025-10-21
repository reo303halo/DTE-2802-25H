namespace TodoApp.Data.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string Name { get; set; } = "Make a todo list.";
    public bool IsDone { get; set; } = false;
}