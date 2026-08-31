using System;
using System.Runtime.CompilerServices;

namespace ToDOEntity;

public class TaskHelper
{
    //Print task
    public static void PrintTask(TaskItem task)
    {
        if (task is null)
        {
            System.Console.WriteLine("ERROR The task is null");
            return;
        }

        string status = task.IsDone ? "Not Done" : "Done";
        System.Console.WriteLine($"[{task.Id}] {task.Title}" +
                                 $"\n   Status: {status}");
    }

    //Print List of tasks
    public static void PrintTasks(List<TaskItem> tasks)
    {
        if (tasks is null)
        {
            System.Console.WriteLine("ERROR The tasks List is null");
            return;
        }

        foreach (var t in tasks)
        {
            PrintTask(t);
        }
    }
}
