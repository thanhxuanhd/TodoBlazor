using System;
using Todo.Application.Features.Categories.Queries.GetCategoryDetail;

namespace Todo.Application.Features.Todo.Queries.GetTodoList;

public class TodoVm
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid TodoId { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryVm Category { get; set; }
}