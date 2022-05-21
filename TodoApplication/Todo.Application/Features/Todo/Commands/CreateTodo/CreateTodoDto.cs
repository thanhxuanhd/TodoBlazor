using System;

namespace Todo.Application.Features.Todo.Commands.CreateTodo;

public class CreateTodoDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid TodoId { get; set; }

    public Guid CategoryId { get; set; }
}
