using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.Contracts;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Persistence.Repositories
{
    internal class TodoRepository : BaseRepository<Entities.Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedList<Entities.Todo>> GetTodoPaginatedList(bool IsCompletedOnly, int pageSize, int pageIndex, string keyword, Guid? categoryId)
        {
            var query = _dbContext.Todos.Where(x => !x.DeletedDate.HasValue);

            if (categoryId.HasValue && categoryId.Value != Guid.Empty)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Title.Contains(keyword));
            }

            if (IsCompletedOnly)
            {
                query = query.Where(x => x.IsCompleted);
            }

            var totalCount = query.Count();

            var listpostCategorys = await query.OrderBy(x => x.Title)
                                         .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                         .AsNoTracking()
                                         .ToListAsync();
            var pages = new PaginatedList<Entities.Todo>()
            {
                PageIndex = pageIndex,
                Items = listpostCategorys,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                TotalRecords = totalCount
            };
            return pages;
        }
    }
}