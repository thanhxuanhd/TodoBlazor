using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.App.Models
{
    public class TodoViewModel
    {
        public Guid TodoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public CategoryViewModel Category { get; set; }

        public Guid CategoryId { get; set; }
    }
}
