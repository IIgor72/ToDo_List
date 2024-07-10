using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Todo_List
{
    internal class Task
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool isCompleted { get; set; }
        
        public Task() { }
        public Task(int taskId, string taskDescription) 
        { 
            id = taskId;
            description = taskDescription;
            isCompleted = false;
        }

        public Task(int taskId, string taskDescription, bool status)
        {
            id = taskId;
            description = taskDescription;
            isCompleted = status;
        }

        public override string ToString()
        {
            return $"{id} | {description} | {(isCompleted ? "Выполнена" : "Не выполнена")}";
        }
        public void InvertStatus()
        {
            isCompleted = !isCompleted;
        }

        internal void UpdateTitle(string newDescription)
        {
            description = newDescription;
        }
    }
}
