using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess;

namespace TaskManager.Business
{
    public interface ITaskManagerHandler
    {
        void AddNewTask(Task task);
        List<Task> GetAllTask();
        List<Task> GetAllParentTask();
        void EditTask(Task task);
        void DeleteTask(Task task);
    }
}
