using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Todo.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeletedTodoCommand, bool>
    {
        private readonly IAsyncRepository<Entities.Todo> _todoRepository;

        public DeleteTodoCommandHandler(IAsyncRepository<Entities.Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(DeletedTodoCommand request, CancellationToken cancellationToken)
        {
            bool deleteTodoSucces = true;

            try
            {
                var todo = await _todoRepository.GetByIdAsync(request.TodoId);

                if (todo == null)
                {
                    deleteTodoSucces = false;
                    // Handle error
                }
                else
                {
                    todo.DeletedDate = DateTime.UtcNow;
                    await _todoRepository.UpdateAsync(todo);
                }
            }
            catch (Exception ex)
            {
                deleteTodoSucces = false;
            }

            return deleteTodoSucces;
        }
    }
}