using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.DTOs;

namespace Todo.Services;

public class TodoService : ITodoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    

    public TodoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }

    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        var todos = await _context.Todos.OrderBy(t => t.Status).ThenBy(t => t.DueDate).ThenBy(t => t.Title).ToListAsync();
        return _mapper.Map<List<TodoDto>>(todos);
    }

    public async Task<TodoDto?> GetByIdAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto> CreateAsync(Models.Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto?> UpdateAsync(int id, Models.Todo todo)
    {
        var existing = await _context.Todos.FindAsync(id);
        if (existing == null) return null;

        existing.Title = todo.Title;
        existing.Description = todo.Description;
        existing.DueDate = todo.DueDate;
        existing.Status = todo.Status;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(id);
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