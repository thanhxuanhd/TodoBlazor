using System.Linq;
using Todo.Application.Contracts.Persistence;
using Entities = Todo.Domain.Entities;

namespace Todo.Persistence.Repositories
{
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
    }
}