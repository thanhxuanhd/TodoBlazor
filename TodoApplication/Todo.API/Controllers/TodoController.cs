using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Application.Contracts;
using Todo.Application.Features.Todo.Commands.CreateTodo;
using Todo.Application.Features.Todo.Commands.DeleteTodo;
using Todo.Application.Features.Todo.Commands.UpdateTodo;
using Todo.Application.Features.Todo.Queries.GetTodoDetail;
using Todo.Application.Features.Todo.Queries.GetTodoList;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Gets", Name = "GetTodos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<TodoVm>>> GetTodos(int pageSize = 20, int pageIndex = 0, string keyword = "", bool isCompletedOnly = false, Guid? categoryId = null)
    {
        var dtos = await _mediator.Send(new GetTodoListQuery()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            Keyword = keyword,
            IsCompletedOnly = isCompletedOnly,
            CategoryId = categoryId
        });
        return Ok(dtos);
    }

    [HttpGet(Name = "GetTodo")]
    public async Task<ActionResult<TodoVm>> GetTodo(Guid todoId)
    {
        var dto = await _mediator.Send(new GetTodoDetailQuery()
        {
            TodoId = todoId
        });

        return Ok(dto);
    }

    [HttpPost(Name = "CreateTodo")]
    public async Task<ActionResult<CreateTodoCommandResponse>> CreateTodo([FromBody] CreateTodoCommand createTodoCommand)
    {
        var todoResponse = await _mediator.Send(createTodoCommand);
        return Ok(todoResponse);
    }

    [HttpPut(Name = "UpdateTodo")]
    public async Task<ActionResult<UpdateTodoCommandResponse>> UpdateTodo([FromBody] UpdateTodoCommand updateTodoCommand)
    {
        var todoResponse = await _mediator.Send(updateTodoCommand);
        return Ok(todoResponse);
    }

    [HttpDelete("{id}", Name = "DeleteTodo")]
    public async Task<ActionResult<bool>> DeleteTodo(Guid id)
    {
        var deletedSuccess = await _mediator.Send(new DeletedTodoCommand() { TodoId = id });
        return Ok(deletedSuccess);
    }
}