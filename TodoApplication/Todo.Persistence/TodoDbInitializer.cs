using System;
using System.Collections.Generic;
using System.Linq;
using Entities = Todo.Domain.Entities;

namespace Todo.Persistence
{
    public class TodoDbInitializer
    {
        public TodoDbInitializer()
        {
        }

        public static void Initializer(TodoDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (!dbContext.Todos.Any() && !dbContext.Categories.Any())
            {
                Entities.Category category = new()
                {
                    Name = "My Todo",
                    CategoryId = Guid.NewGuid()
                };

                Entities.Category category2 = new()
                {
                    Name = "My Todo 2",
                    CategoryId = Guid.NewGuid()
                };

                dbContext.Categories.Add(category);

                dbContext.Categories.Add(category2);

                List<Entities.Todo> todos = new();

                for (int i = 0; i < 100; i++)
                {
                    bool isEven = (i % 2 == 0);
                    Entities.Todo todo = new()
                    {
                        TodoId = Guid.NewGuid(),
                        Title = $"To do {i + 1}",
                        Description = $"{i + 1} Description.",
                        CategoryId = isEven ? category.CategoryId : category2.CategoryId,
                        CreatedDate = DateTime.UtcNow
                    };

                    todos.Add(todo);
                }

                dbContext.Todos.AddRange(todos);

                dbContext.SaveChanges();
            }
        }
    }
}