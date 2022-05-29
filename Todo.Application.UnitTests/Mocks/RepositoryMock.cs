using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Application.Contracts.Persistence;
using Todo.Domain.Entities;
using Todo.Persistence;
using Todo.Persistence.Repositories;

using Entities = Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Mocks;

public class RepositoryMock
{
    private static Guid CategoryIdNeed { get; set; } = Guid.NewGuid();
    private static Guid TodoIdNeed { get; set; } = Guid.NewGuid();

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

        mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid categoryId) =>
        {
            return mockSetCategory.Object.FirstOrDefault(x => x.CategoryId == categoryId);
        });

        return mock;
    }

    public static Mock<IAsyncRepository<Category>> MockAsyncCategoryRepository()
    {
        var categories = SetupCategories().AsQueryable();

        Mock<DbSet<Category>> mockCategories = new();
        mockCategories.As<IAsyncEnumerable<Category>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new TestDbAsyncEnumerator<Category>(categories.GetEnumerator()));

        mockCategories.As<IQueryable<Category>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Category>(categories.Provider));

        mockCategories.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        mockCategories.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        mockCategories.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        var contextOptions = new DbContextOptions<TodoDbContext>();
        var mockContext = new Mock<TodoDbContext>(contextOptions);
        mockContext.Setup(c => c.Set<Category>()).Returns(mockCategories.Object);

        var mockBaseRepository = new Mock<BaseRepository<Category>>(mockContext.Object);

        var mock = new Mock<IAsyncRepository<Category>>();
        mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid categoryId) =>
        {
            return mockCategories.Object.FirstOrDefault(x => x.CategoryId == categoryId);
        })
        .Verifiable();

        return mock;
    }

    public static Mock<IAsyncRepository<Entities.Todo>> MockAsyncTodoRepository()
    {
        var todos = SetupTodos().AsQueryable();

        Mock<DbSet<Entities.Todo>> mockTodos = new();
        mockTodos.As<IAsyncEnumerable<Entities.Todo>>()
            .Setup(m => m.GetAsyncEnumerator(default))
            .Returns(new TestDbAsyncEnumerator<Entities.Todo>(todos.GetEnumerator()));

        mockTodos.As<IQueryable<Entities.Todo>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Category>(todos.Provider));

        mockTodos.As<IQueryable<Entities.Todo>>().Setup(m => m.Expression).Returns(todos.Expression);
        mockTodos.As<IQueryable<Entities.Todo>>().Setup(m => m.ElementType).Returns(todos.ElementType);
        mockTodos.As<IQueryable<Entities.Todo>>().Setup(m => m.GetEnumerator()).Returns(todos.GetEnumerator());

        var contextOptions = new DbContextOptions<TodoDbContext>();
        var mockContext = new Mock<TodoDbContext>(contextOptions);
        mockContext.Setup(c => c.Set<Entities.Todo>()).Returns(mockTodos.Object);

        var mockBaseRepository = new Mock<BaseRepository<Entities.Todo>>(mockContext.Object);

        var mock = new Mock<IAsyncRepository<Entities.Todo>>();
        mock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid todoId) =>
        {
            return mockTodos.Object.FirstOrDefault(x => x.TodoId == todoId);
        })
        .Verifiable();

        mock.Setup(repo => repo.AddAsync(It.IsAny<Entities.Todo>())).ReturnsAsync((Entities.Todo todo)=> {
            todo.TodoId = Guid.NewGuid();
            return todo;
        });

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

    private static List<Entities.Todo> SetupTodos()
    {
        List<Entities.Todo> todos = new()
        {
            new Entities.Todo()
            {
                TodoId = TodoIdNeed,
                Title = "Todo 1",
                CategoryId = CategoryIdNeed
            },
            new Entities.Todo()
            {
                TodoId = Guid.NewGuid(),
                Title = "Todo 2",
                CategoryId = CategoryIdNeed
            },
            new Entities.Todo()
            {
                TodoId = Guid.NewGuid(),
                Title = "Todo 3",
                CategoryId = CategoryIdNeed,
                IsCompleted = true
            },
            new Entities.Todo()
            {
                TodoId = Guid.NewGuid(),
                Title = "Todo 4",
                CategoryId = Guid.NewGuid(),
                IsCompleted = true
            }
        };

        return todos;
    }

    public static Guid GetCategoryId()
    {
        return CategoryIdNeed;
    }

    public static Guid GetTodoId()
    {
        return TodoIdNeed;
    }
}