using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Features.Todo.Queries.GetTodoList;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Todo.Queries.GetTodoDetail
{
    public class GetTodoDetailQueryHandler : IRequestHandler<GetTodoDetailQuery, TodoVm>
    {
        public readonly IAsyncRepository<Entities.Todo> _todoRepository;
        public readonly IMapper _mapper;

        public GetTodoDetailQueryHandler(IAsyncRepository<Entities.Todo> todoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<TodoVm> Handle(GetTodoDetailQuery request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.TodoId);

            if (todo == null)
            {
                throw new Exception("Todo not found");
            }

            todo.Category = new Entities.Category();
            var todoVm = _mapper.Map<TodoVm>(todo);

            return todoVm;
        }
    }
}