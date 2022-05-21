namespace Todo.App.Models;

public class UpdateTodoResponse : BaseResponse
{
    public TodoViewModel Todo { get; set; }
}