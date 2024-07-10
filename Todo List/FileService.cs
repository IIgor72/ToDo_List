using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;


namespace Todo_List
{
    internal class FileService
    {
        public void SaveTasksToFile(List<Task> tasks, string filePath)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    foreach (var task in tasks)
                    {
                        streamWriter.WriteLine(task.ToString());
                    }
                    streamWriter.Close();
                    Console.WriteLine("Задачи успешно сохранены.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач в файл: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        public List<Task> LoadTasksFromFile(List<Task> tasks, string filePath)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string line;
                    Task currentReadTask;
                    tasks.Clear();

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 3)
                        {
                            int id = Convert.ToInt32(parts[0]);
                            string description = parts[1];
                            bool isCompleted = parts[2].Trim() == "Выполнена";
                            currentReadTask = new Task(id, description, isCompleted);
                            tasks.Add(currentReadTask);
                        }
                    }
                    
                    Console.WriteLine("Задачи успешно загружены.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка при преобразовании типов данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                
            }
            return tasks;
        }
        public void SaveTaskToJson(List<Task> tasks, string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач в файл: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        public void LoadTaskFromJson(ref List<Task> tasks, string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<Task>>(json);

            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка при десериализации JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
