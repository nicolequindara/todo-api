using Microsoft.EntityFrameworkCore;

namespace Todo.Services;

public class TodoService : ITodoService
{
    private readonly AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Todo>> GetAllAsync()
    {
        return await _context.Todos.OrderBy(t => t.Status).ThenBy(t => t.DueDate).ThenBy(t => t.Title).ToListAsync();
    }

    public async Task<Models.Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<Models.Todo> CreateAsync(Models.Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<bool> UpdateAsync(int id, Models.Todo todo)
    {
        var existing = await _context.Todos.FindAsync(id);
        if (existing == null) return false;

        existing.Title = todo.Title;
        existing.Description = todo.Description;
        existing.DueDate = todo.DueDate;
        existing.Status = todo.Status;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }
}