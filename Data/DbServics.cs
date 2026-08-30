using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace ToDOEntity;

public class TodoDbService
{
    public async void CheckDb()
    {
        using (var db = new TodoDbContext())
        {
            await db.Database.EnsureCreatedAsync();
        }
        ;
    }

    public async void AddTask(Task task)
    {
        using var db = new TodoDbContext();

        await db.AddAsync(task);
        await db.SaveChangesAsync();
    }

    public async Task<Task?> GetTaskByIdAsync(int id)
    {
        using var db = new TodoDbContext();

        return await db.Set<Task>().FirstOrDefaultAsync(t => t.Id == id);
    }
}