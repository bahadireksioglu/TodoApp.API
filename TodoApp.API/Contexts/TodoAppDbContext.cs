using Microsoft.EntityFrameworkCore;
using TodoApp.API.Entities;
using TodoApp.API.Mappings;

namespace TodoApp.API.Contexts
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options)
        {

        }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMap());
        }
    }
}
