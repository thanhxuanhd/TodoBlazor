using Todo.Application.Responses;

namespace Todo.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandResponse : BaseResponse
{
    public UpdateCategoryCommandResponse() : base()
    {
    }

    public UpdateCategoryDto Category { get; set; }
}