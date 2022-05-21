using MediatR;
using System;
using Todo.Application.Contracts;

namespace Todo.Application.Features.Todo.Queries.GetTodoList;

public class GetTodoListQuery : IRequest<PaginatedList<TodoVm>>
{
    public bool IsCompletedOnly { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public string Keyword { get; set; }

    public Guid? CategoryId { get; set; }
}