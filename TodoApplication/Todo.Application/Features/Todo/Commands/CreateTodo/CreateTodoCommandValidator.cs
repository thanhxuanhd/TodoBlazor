using FluentValidation;

namespace Todo.Application.Features.Todo.Commands.CreateTodo;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(p => p.Title)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(p => p.Description)
             .MaximumLength(5000).WithMessage("{PropertyName} must not exceed 1000 characters.");
    }
}