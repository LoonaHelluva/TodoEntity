using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ToDOEntity
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            //Implementing container
            var collection = new ServiceCollection();

            //Adding DB
            collection.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });

            //Adding DbService with lifetime of Scope (Life span = 1 per DB request)
            collection.AddScoped<DbService>();

            //Building container and saving builder
            var builder = collection.BuildServiceProvider();

            //Executing Scope
            using (var scope = builder.CreateScope())
            {
                //Getting DbService from services via ServiceProvider
                var service = scope.ServiceProvider.GetRequiredService<DbService>();

                //Checking on DB and creating if not exists
                await service.CheckDbAsync();

                //ADD TASK
                System.Console.WriteLine("==== ADDING TASK ====");
                await service.AddTaskAsync(new TaskItem
                {
                    Title = "Make a bed",
                    IsDone = false
                });

                //GETTING and showing all Tasks
                List<TaskItem> tasks = await service.GetTasksAsync();
                TaskHelper.PrintTasks(tasks);
                System.Console.WriteLine("============\n");

                //GETTING Task by id
                System.Console.WriteLine("==== GETTING TASK BY ID ====");
                var task = await service.GetTaskByIdAsync(2);
                TaskHelper.PrintTask(task);
                System.Console.WriteLine("============\n");

                //UPDATING task's title by id
                System.Console.WriteLine("==== UPDATING TASK ====");
                await service.UpdateTaskTitleAsync(2, "Go to the park");
                var tasksList = await service.GetTasksAsync();
                TaskHelper.PrintTasks(tasksList);
                System.Console.WriteLine("============\n");

                //DELETING task by id
                System.Console.WriteLine("==== DELETING TASK ====");
                await service.DeleteTaskById(2);

                var tL = await service.GetTasksAsync();
                TaskHelper.PrintTasks(tL);
                System.Console.WriteLine("============");
            }
        }
    }
}