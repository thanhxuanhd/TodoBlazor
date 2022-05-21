using MediatR;
using System;

namespace Todo.Application.Features.Todo.Commands.DeleteTodo;

public class DeletedTodoCommand : IRequest<bool>
{
    public Guid TodoId { get; set; }
}
