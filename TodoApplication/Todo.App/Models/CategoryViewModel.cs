using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.App.Models
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool CanDelete { get; set; }
    }
}