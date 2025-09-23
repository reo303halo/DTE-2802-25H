using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

[Route("api/[controller]")] // uit.com/api/Todo
[ApiController]
public class TodoController(TodoDbContext context) : ControllerBase
{
    // GET: api/Todo
    // Get all items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
        return await context.TodoItems
            .Select(x => ItemToDto(x))
            .ToListAsync();
    }
    
    // GET: api/Todo/Id => api/Todo/2 
    // Get item by Id
    [HttpGet("{id:long}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
    {
        var todoItem = await context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound(); // Status Code 404: Not Found
        }
        return ItemToDto(todoItem); // Status Code 200: Ok
    }
    
    // POST: api/Todo
    // Create item
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDto)
    {
        var todoItem = new TodoItem
        {
            IsComplete = todoDto.IsComplete,
            Name = todoDto.Name
        };
        
        // Line 47 and 48 typically in repository
        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();
        
        return CreatedAtAction( // Status Code 201: Created
            nameof(GetTodoItem), 
            new { id = todoItem.Id }, 
            ItemToDto(todoItem));
    }
    
    // PUT: api/Todo/4
    // Update item
    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDto)
    {
        if (id != todoDto.Id)
        {
            return BadRequest(); // Status Code 400: Bad Request
        }
        
        var todoItem = await context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        
        todoItem.Name = todoDto.Name;
        todoItem.IsComplete = todoDto.IsComplete;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    // DELETE: api/Todo/Id
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        
        context.TodoItems.Remove(todoItem);
        await context.SaveChangesAsync();
        
        return NoContent(); // Status Code 204: No Content
    }
    
    private static TodoItemDTO ItemToDto(TodoItem item) =>
        new()
        {
            Id = item.Id,
            Name = item.Name,
            IsComplete = item.IsComplete
        };
    
    private bool TodoItemExists(long id) => context.TodoItems.Any(e => e.Id == id);
}

