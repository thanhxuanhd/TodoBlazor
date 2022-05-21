namespace Todo.App.Models;

public class CreateTodoResponse : BaseResponse
{
    public TodoViewModel Todo { get; set; }
}