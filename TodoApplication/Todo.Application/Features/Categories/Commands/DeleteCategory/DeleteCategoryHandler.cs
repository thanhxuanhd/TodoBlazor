using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Contracts.Persistence;
using Todo.Application.Exceptions;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IAsyncRepository<Entities.Category> _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(IAsyncRepository<Entities.Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (categoryToDelete == null)
            {
                throw new NotFoundException(nameof(Entities.Category), request.CategoryId);
            }

            categoryToDelete.DeletedDate = DateTime.Now;
            await _categoryRepository.UpdateAsync(categoryToDelete);

            return Unit.Value;
        }
    }
}