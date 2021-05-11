using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Todo.Application.Features.Todo.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeletedTodoCommand>
    {
        public Task<Unit> Handle(DeletedTodoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}