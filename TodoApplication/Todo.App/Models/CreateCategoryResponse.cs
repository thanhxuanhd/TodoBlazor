namespace Todo.App.Models;

public class CreateCategoryResponse : BaseResponse
{
    public CategoryViewModel Category { get; set; }
}