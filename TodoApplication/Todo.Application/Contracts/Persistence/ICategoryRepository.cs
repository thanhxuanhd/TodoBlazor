using Entities = Todo.Domain.Entities;
using System.Linq;

namespace Todo.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Entities.Category>
    {
        bool CheckCategoryDuplicate(string name);

        IQueryable<Entities.Category> GetAll();
    }
}