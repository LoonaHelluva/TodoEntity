using System;
using Microsoft.EntityFrameworkCore;

namespace ToDOEntity;

public class TodoDbContext : DbContext
{
    public DbSet<Task> Task { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TodoDb.db")
                      .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }
}
