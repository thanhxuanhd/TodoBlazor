using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.App.Contracts;
using Todo.App.Models;

namespace Todo.App.Services
{
    public class CategoryDataService : BaseDataService, ICategoryDataService
    {
        private readonly IMapper _mapper;

        public CategoryDataService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<CreateCategoryResponse> CreateCategory(CategoryViewModel category)
        {
            var categoryDTO = _mapper.Map<CreateCategoryCommand>(category);

            var categoryResponse = await _client.CreateCategoryAsync(categoryDTO);

            var mapCategoryResponse = _mapper.Map<CreateCategoryResponse>(categoryResponse);

            return mapCategoryResponse;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var allCategories = await _client.GetAllCategoriesAsync();
            var mappedCategories = _mapper.Map<ICollection<CategoryViewModel>>(allCategories);
            return mappedCategories.ToList();
        }

        public async Task<CategoryViewModel> GetCategory(Guid id)
        {
            var category = await _client.GetCategoryAsync(id);

            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task UpdapteCategory(CategoryViewModel category)
        {
            var categoryDto = _mapper.Map<UpdateCategoryCommand>(category);
            await _client.UpdateCategoryAsync(categoryDto);
        }
    }
}