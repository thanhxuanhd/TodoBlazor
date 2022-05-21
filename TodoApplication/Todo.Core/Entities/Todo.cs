using System;
using Todo.Domain.Common;

namespace Todo.Domain.Entities;

public class Todo : AuditableEntity
{
    public Guid TodoId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public Category Category { get; set; }

    public Guid CategoryId { get; set; }
}