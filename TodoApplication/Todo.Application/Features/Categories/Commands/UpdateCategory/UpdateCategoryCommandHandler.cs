using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCategoryCommandValidator(_categoryRepository);
        var updateCategoryResponse = new UpdateCategoryCommandResponse();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            updateCategoryResponse.Success = false;
            updateCategoryResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                updateCategoryResponse.ValidationErrors.Add(error.ErrorMessage);
            }
            updateCategoryResponse.Category = _mapper.Map<UpdateCategoryDto>(request);

            return updateCategoryResponse;
        }

        var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (categoryToUpdate == null)
        {
            updateCategoryResponse.Success = false;
            updateCategoryResponse.ValidationErrors.Add($"Category not found id {request.CategoryId}");
        }
        else
        {
            _mapper.Map(request, categoryToUpdate, typeof(UpdateCategoryCommand), typeof(Entities.Category));

            await _categoryRepository.UpdateAsync(categoryToUpdate);
        }

        return updateCategoryResponse;
    }
}