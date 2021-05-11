using FluentValidation;
using Todo.Application.Contracts.Persistence;

namespace Todo.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 10 characters.")
                .Must((name) => categoryRepository.CheckCategoryDuplicate(name));
        }
    }
}