using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Todo.Application.Contracts.Persistence;
using Todo.Domain.Entities;
using Entities = Todo.Domain.Entities;

namespace Todo.Persistence.Repositories;

[ExcludeFromCodeCoverage]
public class CategoryRepository : BaseRepository<Entities.Category>, ICategoryRepository
{
    public CategoryRepository(TodoDbContext dbContext) : base(dbContext)
    {
    }

    public bool CheckCategoryDuplicate(string name)
    {
        var isDuplicate = _dbContext.Categories.Where(x => !x.DeletedDate.HasValue)
        .Any(x => x.Name.Equals(name));

        return isDuplicate;
    }

    public IQueryable<Category> GetAll()
    {
        return _dbContext.Categories;
    }
}