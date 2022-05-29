using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Todo.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, CreateTodoCommandResponse>
{
    private readonly IAsyncRepository<Entities.Todo> _todoRepository;
    private readonly IMapper _mapper;

    public CreateTodoCommandHandler(IAsyncRepository<Entities.Todo> todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<CreateTodoCommandResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var createTodoCommandResponse = new CreateTodoCommandResponse();

        var validator = new CreateTodoCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            createTodoCommandResponse.Success = false;
            createTodoCommandResponse.ValidationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            createTodoCommandResponse.Todo = _mapper.Map<CreateTodoDto>(request);
        }
        else
        {
            var todo = _mapper.Map<Entities.Todo>(request);
            todo = await _todoRepository.AddAsync(todo);
            createTodoCommandResponse.Todo = _mapper.Map<CreateTodoDto>(todo);
        }

        return createTodoCommandResponse;
    }
}