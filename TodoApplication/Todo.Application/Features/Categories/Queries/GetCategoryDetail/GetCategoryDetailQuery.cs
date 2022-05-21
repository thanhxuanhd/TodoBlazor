using MediatR;
using System;

namespace Todo.Application.Features.Categories.Queries.GetCategoryDetail;

public class GetCategoryDetailQuery : IRequest<CategoryVm>
{
    public Guid CategoryId { get; set; }
}