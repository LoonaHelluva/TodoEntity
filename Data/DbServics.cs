using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ToDOEntity;

public class DbService
{
    private readonly TodoDbContext _db;

    public DbService(TodoDbContext db)
    {
        _db = db;
    }

    //Checking on data base
    public async Task CheckDbAsync()
    {
        var isExists = await _db.Database.EnsureCreatedAsync();

        if (isExists == false)
        {
            await AddTaskAsync(new List<TaskItem>()
                {
                    new TaskItem
                    {
                        Title = "Buy a milk",
                        IsDone = false
                    },
                    new TaskItem
                    {
                        Title = "Go to carting",
                        IsDone = false
                    }
                });
        }
    }

    //Add task
    public async Task AddTaskAsync(TaskItem task)
    {
        await _db.AddAsync(task);
        await _db.SaveChangesAsync();
    }

    //Add task's list OVERLOAD
    public async Task AddTaskAsync(List<TaskItem> tasks)
    {
        await _db.AddRangeAsync(tasks);
        await _db.SaveChangesAsync();
    }

    //Get task by id
    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _db.Tasks
                        .Where(t => t.IsDone == false)
                        .FirstOrDefaultAsync(t => t.Id == id);
    }

    //Get tasks
    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await _db.Tasks.AsNoTracking<TaskItem>().ToListAsync<TaskItem>();
    }

    //Update task title by id
    public async Task UpdateTaskTitleAsync(int id, string newTitle)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            System.Console.WriteLine("ERROR Task is null");
            return;
        }

        task.Title = newTitle;

        await _db.SaveChangesAsync();
    }

    //Complete task by id   
    public async Task CompleteTaskAsync(int id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            return;
        }

        task.IsDone = false ? false : true;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteTaskById(int id)
    {
        await _db.Tasks.Where(t => t.Id == id).ExecuteDeleteAsync();
    }
}