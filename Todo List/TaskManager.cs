using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_List
{
    internal class TaskManager
    {
        private List<Task> Tasks;
        private int countTasks = 0;

        public TaskManager()
        {
            Tasks = new List<Task>();
        }
        public TaskManager(List<Task> _tasks)
        {
            Tasks = _tasks;
        }

        public void AddTask(string taskDescription)
        {
            Task taskForAdd = new Task(countTasks+1, taskDescription);
            Tasks.Add(taskForAdd);
            Console.WriteLine("Задача добавлена в список.");
        }
        public void RemoveTask(int taskId)
        {
            var deletedTask = Tasks.FirstOrDefault(x => x.id == taskId);
            if (deletedTask != null)
            {
                Tasks.Remove(deletedTask);
                countTasks--;
                Console.WriteLine("Задача успешно удалена");
            }
            else
            {
                Console.WriteLine("Задача с таким номером не найдена.");
            }
        }

        public List<Task> GetAllTasks()
        {
            return Tasks;
        }

        public void SetCountTasks(int _count)
        {
            countTasks = _count;
            Console.WriteLine(countTasks);
        }
        public List<Task> FilteringByStatus(bool status)
        {
            return Tasks.Where(x => x.isCompleted == status).ToList();
        }
        public void ChangeStatusTask(int taskId)
        {
            var task = Tasks.FirstOrDefault(x => x.id == taskId);
            if (task != null)
            {
                task.InvertStatus();
                Console.WriteLine("Статус изменен:");
                Console.WriteLine(task.ToString());
            }
            else
            {
                Console.WriteLine("Задача с указанным ID не найдена.");
            }
        }
        public void UpdateTaskTitle(int taskId, string newDescription)
        {
            var task = Tasks.FirstOrDefault(t => t.id == taskId);
            if (task != null)
            {
                task.UpdateTitle(newDescription);
                Console.WriteLine("Описание изменено:");
                Console.WriteLine(task.ToString());
            }
            else
            {
                Console.WriteLine("Задача с указанным ID не найдена.");
            }
        }

    }
}

