using System;

namespace Todo.Application.Features.Categories.Queries.GetCategoryDetail
{
    public class CategoryVm
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public bool CanDeleteCategory { get; set; }
    }
}