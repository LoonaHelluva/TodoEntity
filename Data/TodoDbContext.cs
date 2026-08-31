using System;
using Microsoft.EntityFrameworkCore;

namespace ToDOEntity;

public class TodoDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }
}
