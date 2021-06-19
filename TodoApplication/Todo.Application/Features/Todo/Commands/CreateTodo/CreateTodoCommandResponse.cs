using Todo.Application.Responses;

namespace Todo.Application.Features.Todo.Commands.CreateTodo
{
    public class CreateTodoCommandResponse : BaseResponse
    {
        public CreateTodoCommandResponse() : base()
        {
        }

        public CreateTodoDto Todo { get; set; }
    }
}