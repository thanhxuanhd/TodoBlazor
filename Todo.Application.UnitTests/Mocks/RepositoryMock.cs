using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Application.Contracts.Persistence;
using Todo.Domain.Entities;
using Todo.Persistence;
using Todo.Persistence.Repositories;

namespace Todo.Application.UnitTests.Mocks;

public class RepositoryMock
{
    private static Guid CategoryIdNeed { get; set; } = Guid.NewGuid();

    public static Mock<ICategoryRepository> MockCategoryRepository()
    {
        var categories = SetupCategories().AsQueryable();
        var mockSetCategory = new Mock<DbSet<Category>>();

        mockSetCategory.As<IAsyncEnumerable<Category>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new TestDbAsyncEnumerator<Category>(categories.GetEnumerator()));

        mockSetCategory.As<IQueryable<Category>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Category>(categories.Provider));

        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        var mock = new Mock<ICategoryRepository>();

        mock.Setup(repo => repo.GetAll()).Returns(mockSetCategory.Object);

        mock.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync((Category category) =>
        {
            category.CategoryId = Guid.NewGuid();
            return category;
        });

        return mock;
    }

    public static Mock<IAsyncRepository<Category>> MockAsyncCategoryRepository()
    {
        var categories = SetupCategories().AsQueryable();

        Mock<DbSet<Category>> mockSetCategory = new();
        mockSetCategory.As<IAsyncEnumerable<Category>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new TestDbAsyncEnumerator<Category>(categories.GetEnumerator()));

        mockSetCategory.As<IQueryable<Category>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Category>(categories.Provider));

        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        mockSetCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        var contextOptions = new DbContextOptions<TodoDbContext>();
        var mockContext = new Mock<TodoDbContext>(contextOptions);
        mockContext.Setup(c => c.Set<Category>()).Returns(mockSetCategory.Object);

        var mockBaseRepository = new Mock<BaseRepository<Category>>(mockContext.Object);

        var mock = new Mock<IAsyncRepository<Category>>();
        mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid categoryId) =>
        {
            return mockSetCategory.Object.FirstOrDefault(x => x.CategoryId == categoryId);
        })
        .Verifiable();

        return mock;
    }

    private static List<Category> SetupCategories()
    {
        List<Category> categories = new();
        Category category = new()
        {
            Name = "My Todo",
            CategoryId = CategoryIdNeed,
            CreatedDate = DateTime.UtcNow,
            Todos = new List<Todo.Domain.Entities.Todo>()
        };


        Category category2 = new()
        {
            Name = "My Todo 2",
            CategoryId = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            Todos = new List<Todo.Domain.Entities.Todo>()
        };

        categories.Add(category);

        categories.Add(category2);

        return categories;
    }

    private static List<Todo.Domain.Entities.Todo> SetupTodos()
    {
        List<Todo.Domain.Entities.Todo> todos = new();

        return todos;
    }

    public static Guid GetCategoryId()
    {
        return CategoryIdNeed;
    }

}
