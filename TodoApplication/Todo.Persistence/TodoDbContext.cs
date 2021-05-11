using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Common = Todo.Domain.Common;
using Entities = Todo.Domain.Entities;

namespace Todo.Persistence
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Todo> Todos { get; set; }

        public DbSet<Entities.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);

            // Table Todo
            modelBuilder.Entity<Entities.Todo>().HasKey(x => x.TodoId);
            modelBuilder.Entity<Entities.Todo>().Property(x => x.Title)
                .HasMaxLength(5000)
                .IsRequired();

            // Table Categories
            modelBuilder.Entity<Entities.Category>().Property(x => x.Name)
                .HasMaxLength(1000)
                .IsRequired();

            //
            modelBuilder.Entity<Entities.Todo>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Todos)
                .HasForeignKey(x => x.CategoryId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Common.AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}