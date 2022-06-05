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
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IAsyncRepository<Entities.Category> _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IAsyncRepository<Entities.Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            bool deletedSuccess = true;
            try
            {
                var categoryToDelete = await _categoryRepository.GetByIdAsync(request.CategoryId);

                if (categoryToDelete == null)
                {
                    throw new NotFoundException(nameof(Entities.Category), request.CategoryId);
                }

                categoryToDelete.DeletedDate = DateTime.Now;
                await _categoryRepository.UpdateAsync(categoryToDelete);
            }
            catch (Exception)
            {
                deletedSuccess = false;
            }

            return deletedSuccess;
        }
    }
}