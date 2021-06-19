using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Features.Todo.Commands.CreateTodo
{
    public class CreateTodoCommand: IRequest<CreateTodoCommandResponse>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public Guid TodoId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
