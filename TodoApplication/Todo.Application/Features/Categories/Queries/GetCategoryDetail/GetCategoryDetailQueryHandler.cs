using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Exceptions;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryVm>
    {
        public readonly IAsyncRepository<Entities.Category> _categoryRepository;
        public readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IAsyncRepository<Entities.Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryVm> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var @category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (@category == null)
            {
                throw new NotFoundException(nameof(Entities.Category), request.CategoryId);
            }

            var categoryVm = _mapper.Map<CategoryVm>(@category);

            return categoryVm;
        }
    }
}