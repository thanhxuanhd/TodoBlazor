using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.App.Models
{
    public class TodoViewModel
    {
        public Guid TodoId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public CategoryViewModel? Category { get; set; }

        public Guid? CategoryId { get; set; }
    }
}