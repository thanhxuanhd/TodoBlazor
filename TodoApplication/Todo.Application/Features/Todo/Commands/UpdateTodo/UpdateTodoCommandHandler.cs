using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Todo.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, UpdateTodoCommandResponse>
{
    private readonly IAsyncRepository<Entities.Todo> _todoRepository;
    private readonly IMapper _mapper;

    public UpdateTodoCommandHandler(IAsyncRepository<Entities.Todo> todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<UpdateTodoCommandResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var updateTodoCommandResponse = new UpdateTodoCommandResponse();
        var validator = new UpdateTodoCommandValidator();

        var todoUpdate = await _todoRepository.GetByIdAsync(request.TodoId);

        if (todoUpdate == null)
        {
            updateTodoCommandResponse.Success = false;
            updateTodoCommandResponse.ValidationErrors.Add($"Todo not found id {request.TodoId}");
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            updateTodoCommandResponse.Success = false;
            updateTodoCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                updateTodoCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
            updateTodoCommandResponse.Todo = _mapper.Map<UpdateTodoDto>(todoUpdate);
        }
        else
        {
            _mapper.Map(request, todoUpdate, typeof(UpdateTodoCommand), typeof(Entities.Todo));

            await _todoRepository.UpdateAsync(todoUpdate);
            updateTodoCommandResponse.ValidationErrors = new List<string>();
            updateTodoCommandResponse.Success = true;
            updateTodoCommandResponse.Todo = _mapper.Map<UpdateTodoDto>(todoUpdate);
        }

        return updateTodoCommandResponse;
    }
}