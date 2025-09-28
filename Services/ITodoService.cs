using Todo.DTOs;
using Todo.Models;

namespace Todo.Services;
using Models;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllAsync();
    Task<TodoDto?> GetByIdAsync(int id);
    Task<TodoDto> CreateAsync(Todo todo);
    Task<TodoDto?> UpdateAsync(int id, Todo todo);
    Task<bool> DeleteAsync(int id);
}