using System;
using Microsoft.EntityFrameworkCore;

namespace ToDOEntity;

public class DbService
{
    private readonly TodoDbContext _db;

    public DbService(TodoDbContext db)
    {
        _db = db;
    }
    public async Task CheckDbAsync()
    {
        await _db.Database.EnsureCreatedAsync();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _db.AddAsync(task);
        await _db.SaveChangesAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _db.Set<TaskItem>()
                        .Where(t => t.IsDone == false)
                        .FirstOrDefaultAsync(t => t.Id == id);
    }
}