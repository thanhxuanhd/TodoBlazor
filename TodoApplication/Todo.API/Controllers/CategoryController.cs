using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Features.Categories.Commands.CreateCategory;
using Todo.Application.Features.Categories.Commands.DeleteCategory;
using Todo.Application.Features.Categories.Commands.UpdateCategory;
using Todo.Application.Features.Categories.Queries.GetCategoriesList;
using Todo.Application.Features.Categories.Queries.GetCategoryDetail;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Gets", Name = "GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
    {
        var dtos = await _mediator.Send(new GetCategoriesListQuery());
        return Ok(dtos);
    }

    [HttpGet("Get", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryListVm>> GetCategory(Guid categoryId)
    {
        var category = await _mediator.Send(new GetCategoryDetailQuery()
        {
            CategoryId = categoryId
        });
        return Ok(category);
    }

    [HttpPost(Name = "CreateCategory")]
    public async Task<ActionResult<CreateCategoryCommandResponse>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        var categoryCommandResponse = await _mediator.Send(createCategoryCommand);
        return Ok(categoryCommandResponse);
    }

    [HttpPut(Name = "UpdateCategory")]
    public async Task<ActionResult<UpdateCategoryCommandResponse>> UpdateCategory([FromBody] UpdateCategoryCommand updateCategoryCommand)
    {
        var updateReponse = await _mediator.Send(updateCategoryCommand);
        return Ok(updateReponse);
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public async Task<ActionResult<bool>> DeleteCategory(Guid id)
    {
        var deleteCategoryCommand = new DeleteCategoryCommand() { CategoryId = id };
        var deletedSuccess = await _mediator.Send(deleteCategoryCommand);
        return Ok(deletedSuccess);
    }
}