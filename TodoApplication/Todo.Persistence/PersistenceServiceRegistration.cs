using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Todo.Application.Contracts.Persistence;
using Todo.Persistence.Repositories;

namespace Todo.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TodoConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }

        public static IHost AddPersistenceSeedData(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<TodoDbContext>();
                    TodoDbInitializer.Initializer(dbContext);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return webHost;
        }
    }
}