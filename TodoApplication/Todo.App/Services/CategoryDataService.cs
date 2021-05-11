﻿using AutoMapper;
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

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var allCategories = await _client.GetAllCategoriesAsync();
            var mappedCategories = _mapper.Map<ICollection<CategoryViewModel>>(allCategories);
            return mappedCategories.ToList();
        }
    }
}