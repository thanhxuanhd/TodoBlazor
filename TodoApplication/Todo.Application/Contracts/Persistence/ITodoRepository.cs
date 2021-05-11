using System;
using System.Threading.Tasks;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Contracts.Persistence
{
    public interface ITodoRepository : IAsyncRepository<Entities.Todo>
    {
        public Task<PaginatedList<Entities.Todo>> GetTodoPaginatedList(bool IsCompletedOnly, int pageSize, int pageIndex, string keyword, Guid? categoryId);
    }
}