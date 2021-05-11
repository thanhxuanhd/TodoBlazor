using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Application.Contracts;
using Todo.Application.Features.Todo.Commands.CreateTodo;
using Todo.Application.Features.Todo.Commands.UpdateTodo;
using Todo.Application.Features.Todo.Queries.GetTodoList;

namespace Todo.API.Controllers
{
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

        [HttpGet("Get", Name = "Get")]
        public IActionResult Get(Guid todoId)
        {
            return Ok(todoId);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateTodoCommand createTodoCommand)
        {
            return Ok(createTodoCommand);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] UpdateTodoCommand updateTodoCommand)
        {
            return Ok(updateTodoCommand);
        }


        [HttpPut("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(id);
        }
    }
}