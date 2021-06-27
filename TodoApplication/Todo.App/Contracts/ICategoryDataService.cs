using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.App.Models;
using Todo.App.Services;

namespace Todo.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetAllCategories();

        Task<CategoryViewModel> GetCategory(Guid id);

        Task<CreateCategoryResponse> CreateCategory(CategoryViewModel category);

        Task<UpdateCategoryReponse> UpdapteCategory(CategoryViewModel category);

        Task<bool> DeleteCategory(Guid categoryId);
    }
}