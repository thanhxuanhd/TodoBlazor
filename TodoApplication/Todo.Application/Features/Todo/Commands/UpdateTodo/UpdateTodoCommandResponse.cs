using Todo.Application.Responses;

namespace Todo.Application.Features.Todo.Commands.UpdateTodo
{
    public class UpdateTodoCommandResponse : BaseResponse
    {
        public UpdateTodoDto Todo { get; set; }
    }
}