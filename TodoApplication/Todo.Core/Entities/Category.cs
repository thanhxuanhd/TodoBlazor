using System;
using System.Collections.Generic;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}