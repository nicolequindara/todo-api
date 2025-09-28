using Todo.DTOs;
using AutoMapper;
namespace Todo.Mapping;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<Models.Todo, TodoDto>();   // Model -> DTO
        CreateMap<TodoDto, Models.Todo>();   // DTO -> Model
    }
}