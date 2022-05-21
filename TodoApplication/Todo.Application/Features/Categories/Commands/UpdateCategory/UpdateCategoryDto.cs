using System;

namespace Todo.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
}