using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Todo.Commands.CreateTodo
{
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

            if (validationResult.Errors.Count > 0)
            {
                createTodoCommandResponse.Success = false;
                createTodoCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createTodoCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (createTodoCommandResponse.Success)
            {
                var todo = new Entities.Todo() { Title = request.Title };
                todo = await _todoRepository.AddAsync(todo);
                createTodoCommandResponse.Todo = _mapper.Map<CreateTodoDto>(todo);
            }

            return createTodoCommandResponse;
        }
    }
}