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

                dbContext.Categories.Add(category);

                List<Entities.Todo> todos = new();

                for (int i = 0; i < 100; i++)
                {
                    Entities.Todo todo = new()
                    {
                        TodoId = Guid.NewGuid(),
                        Title = $"To do {i + 1}",
                        Description = $"{i + 1} Description.",
                        CategoryId = category.CategoryId
                    };

                    todos.Add(todo);
                }

                dbContext.Todos.AddRange(todos);

                dbContext.SaveChanges();
            }
        }
    }
}