using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.App.Models;

namespace Todo.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetAllCategories();

        Task<CategoryViewModel> GetCategory(Guid id);

        Task<Guid> CreateCategory(CategoryViewModel category);

        Task UpdapteCategory(CategoryViewModel category);
    }
}