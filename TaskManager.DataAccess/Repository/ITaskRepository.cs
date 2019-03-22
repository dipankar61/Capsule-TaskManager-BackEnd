using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess
{
    public interface ITaskRepository
    {
        void AddTask(Task task);
        void EditTask(Task task);
        IQueryable<Task> GetAllTask();
        void DeleteTask(Task task);
        Task GetTaskByID(int taskID);
        Task GetTaskByName(string name);
    }
}
