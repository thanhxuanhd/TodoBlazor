using FluentValidation;
using System;
using Todo.Application.Contracts.Persistence;

namespace Todo.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.")
                .Must((name) => !categoryRepository.CheckCategoryDuplicate(name)).WithMessage("{PropertyName} is duplicate.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty);
        }
    }
}