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

namespace Todo.API.Controllers
{
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
        public async Task<ActionResult<Guid>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var categoryCommandResponse = await _mediator.Send(createCategoryCommand);
            return Ok(categoryCommandResponse);
        }

        [HttpPut(Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            await _mediator.Send(updateCategoryCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand() { CategoryId = id };
            await _mediator.Send(deleteCategoryCommand);
            return NoContent();
        }
    }
}