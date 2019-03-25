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
        List<TaskView> GetAllTask();
        List<TaskView> GetAllParentTask();
        void EditTask(Task task);
        void DeleteTask(Task task);
    }
}
