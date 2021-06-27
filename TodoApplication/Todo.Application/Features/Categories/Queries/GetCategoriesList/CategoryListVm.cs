using System;

namespace Todo.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoryListVm
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool CanDelete { get; set; }
    }
}