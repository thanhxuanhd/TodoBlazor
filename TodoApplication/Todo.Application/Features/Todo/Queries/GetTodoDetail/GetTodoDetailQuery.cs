using MediatR;
using System;
using Todo.Application.Features.Todo.Queries.GetTodoList;

namespace Todo.Application.Features.Todo.Queries.GetTodoDetail;

public class GetTodoDetailQuery : IRequest<TodoVm>
{
    public Guid TodoId { get; set; }
}