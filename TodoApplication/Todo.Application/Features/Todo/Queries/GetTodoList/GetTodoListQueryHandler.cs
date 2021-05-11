using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts;
using Todo.Application.Contracts.Persistence;

namespace Todo.Application.Features.Todo.Queries.GetTodoList
{
    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, PaginatedList<TodoVm>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodoListQueryHandler(IMapper mapper, ITodoRepository todoRepository)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<PaginatedList<TodoVm>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            var todos = (await _todoRepository.GetTodoPaginatedList(request.IsCompletedOnly, request.PageSize, request.PageIndex, request.Keyword, request.CategoryId));
            return _mapper.Map<PaginatedList<TodoVm>>(todos);
        }
    }
}