using FluentValidation;
using System;

namespace Todo.Application.Features.Todo.Commands.UpdateTodo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(p => p.Title)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(p => p.Description)
             .MaximumLength(5000).WithMessage("{PropertyName} must not exceed 1000 characters.");

        RuleFor(p => p.TodoId)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
    }
}