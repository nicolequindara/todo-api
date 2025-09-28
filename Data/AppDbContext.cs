using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Todo.Models.Todo> Todos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}