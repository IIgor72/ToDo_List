namespace Todo_List
{
    internal class Program
    {
        private static TaskManager taskManager = new TaskManager();
        private static FileService fileService = new FileService();
        static void Main(string[] args)
        {
            StartLoadList();
            while (true)
            {
                DisplayMenu();
                HandleUserInput();
            }
        }

        private static void StartLoadList()
        {
            List<Task> tasks = taskManager.GetAllTasks();
            fileService.LoadTaskFromJson(ref tasks, "TodoList.json");
            taskManager = new TaskManager(tasks);
            taskManager.SetCountTasks(tasks.Count);
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Todo List");
            Console.WriteLine("1. Добавление задачи в список");
            Console.WriteLine("2. Удаление задачи из списка");
            Console.WriteLine("3. Просмотр списка задач");
            Console.WriteLine("4. Сохранение в файл");
            Console.WriteLine("5. Загрузка из файла");
            Console.WriteLine("6. Изменить статус задачи");
            Console.WriteLine("7. Вывод по статусу");
            Console.WriteLine("8. Редактирование задачи");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }

        static void HandleUserInput()
        {
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    if (taskManager.GetAllTasks()!=null && taskManager.GetAllTasks().Count() > 0)
                    {
                        fileService.SaveTaskToJson(taskManager.GetAllTasks(), "TodoList.json");
                    }
                    Environment.Exit(0);
                    break;
                case "1":
                    Console.Write("Введите задачу: ");
                    string description = Console.ReadLine();
                    taskManager.AddTask(description);
                    break;
                case "2":
                    Console.Write("Введите номер задачи для удаления: ");
                    int deleteId = Convert.ToInt32(Console.ReadLine());
                    taskManager.RemoveTask(deleteId);
                    break;
                case "3":
                    ViewTasksList(taskManager.GetAllTasks());
                    break;
                case "4":
                    string saveFilename;
                    Console.Write("Введите название файла для сохранения: ");
                    saveFilename = Console.ReadLine();
                    fileService.SaveTasksToFile(taskManager.GetAllTasks(), saveFilename + ".txt");
                    break;
                case "5":
                    string LoadFilename;
                    Console.Write("Введите название файла для загрузки: ");
                    LoadFilename = Console.ReadLine();
                    fileService.LoadTasksFromFile(taskManager.GetAllTasks(), LoadFilename + ".txt");
                    break;
                case "6":
                    Console.Write("Выберите id записи, для которой необходимо сменить статус: ");
                    int correctionIdForStatus = Convert.ToInt32(Console.ReadLine());
                    taskManager.ChangeStatusTask(correctionIdForStatus);
                    break;
                case "7":
                    Console.Write("Для вывода отсортированного списка," +
                        " введите статус задачи (Выполнено/Не выполнено) - ");
                    string statusEnteredByUser = Console.ReadLine();
                    Console.WriteLine();
                    bool filterStatus = (statusEnteredByUser == "Выполнено") ? true : false;
                    var filtredList = taskManager.FilteringByStatus(filterStatus);
                    ViewTasksList(filtredList);
                    break;
                case "8":
                    Console.Write("Выберите id записи, для которой необходимо обновить описание: ");
                    int correctionIdForDescription = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Введите новое описание задачи: ");
                    string updateDescription = "  " + Console.ReadLine();  //пробел для сохранения расположения в таблице при выводе
                    Console.WriteLine();
                    taskManager.UpdateTaskTitle(correctionIdForDescription, updateDescription);
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
            Console.WriteLine();
        }

        public static void ViewTasksList(List<Task> Tasks)
        {
            try
            {
                int maxIdLength = Tasks.Max(task => task.id.ToString().Length);
                int maxDescriptionLength = Tasks.Max(task => task.description.Length);
                int maxStatusLength = Tasks.Max(task => (task.isCompleted ? "Выполнена" : "Не выполнена").Length);

                string separator = new string('-', maxIdLength + maxDescriptionLength + maxStatusLength + 10);
                /*число 10 для нормального вывода таблицы из-за разделителей между колонками*/

                Console.WriteLine("Список задач");
                foreach (var task in Tasks)
                {
                    Console.WriteLine(separator);
                    Console.WriteLine($"| {task.id.ToString().PadRight(maxIdLength)} |" +
                        $" {task.description.PadRight(maxDescriptionLength)} |" +
                        $" {(task.isCompleted ? "Выполнена" : "Не выполнена").PadRight(maxStatusLength)} |");
                }
                Console.WriteLine(separator);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Сначала создайте список для вывода.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
