using Entities = Todo.Domain.Entities;

namespace Todo.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Entities.Category>
    {
        bool CheckCategoryDuplicate(string name);
    }
}