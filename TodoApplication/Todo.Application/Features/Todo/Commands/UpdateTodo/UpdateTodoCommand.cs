﻿using MediatR;
using System;

namespace Todo.Application.Features.Todo.Commands.UpdateTodo;

public class UpdateTodoCommand : IRequest<UpdateTodoCommandResponse>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public Guid TodoId { get; set; }

    public Guid CategoryId { get; set; }
}
