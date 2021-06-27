using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Todo.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListVm>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryListVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = _categoryRepository.GetAll().Where(x => !x.DeletedDate.HasValue);
            var categories = await allCategories.Include(x => x.Todos)
                .Select(x => new CategoryListVm()
                {
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    CanDelete = x.Todos.Count == 0
                })
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken: cancellationToken);
            return categories;
        }
    }
}