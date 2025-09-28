using Microsoft.AspNetCore.Mvc;
using Todo.Services;
namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Todo>>> GetTodos()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Todo>> GetTodo(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Models.Todo>> PostTodo(Models.Todo todo)
    {
        var created = await _todoService.CreateAsync(todo);
        return CreatedAtAction(nameof(GetTodo), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(int id, Models.Todo todo)
    {
        var updated = await _todoService.UpdateAsync(id, todo);
        todo = await _todoService.GetByIdAsync(id);
        return updated ? Ok(todo) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var deleted = await _todoService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}