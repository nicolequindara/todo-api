using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.DTOs;
using Todo.Services;
namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;

    public TodosController(ITodoService todoService, IMapper mapper)
    {
        _todoService = todoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Todo>>> GetTodos()
    {
        var todos = await _todoService.GetAllAsync();
        var dtos = _mapper.Map<List<TodoDto>>(todos);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Todo>> GetTodo(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        var dto = _mapper.Map<TodoDto>(todo);
        return todo == null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Models.Todo>> PostTodo(Models.Todo todo)
    {
        var created = await _todoService.CreateAsync(todo);
        var dto = _mapper.Map<TodoDto>(created);
        return CreatedAtAction(nameof(GetTodo), new { id = created.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(int id, Models.Todo todo)
    {
        var updated = await _todoService.UpdateAsync(id, todo);
        todo = await _todoService.GetByIdAsync(id);
        var dto = _mapper.Map<TodoDto>(todo);
        return updated ? Ok(dto) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var deleted = await _todoService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}