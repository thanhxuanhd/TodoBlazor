using MediatR;
using System;

namespace Todo.Application.Features.Todo.Commands.CreateTodo;

public class CreateTodoCommand : IRequest<CreateTodoCommandResponse>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid TodoId { get; set; }

    public Guid CategoryId { get; set; }
}
