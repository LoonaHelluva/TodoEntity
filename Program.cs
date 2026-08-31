using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ToDOEntity
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var collection = new ServiceCollection();

            collection.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });

            collection.AddScoped<DbService>();

            var builder = collection.BuildServiceProvider();

            using (var scope = builder.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<DbService>();
                await service.CheckDbAsync();

                await service.AddTaskAsync(new TaskItem
                {
                    Title = "Milk",
                    IsDone = false
                });

                var task = await service.GetTaskByIdAsync(1);

                if (task != null)
                {
                    System.Console.WriteLine($"Created the Task:" + $"\nTitle: {task.Title}");
                    return;
                }
                else
                {
                    System.Console.WriteLine("Some error ocured, task was not created");
                }
            }
        }
    }
}