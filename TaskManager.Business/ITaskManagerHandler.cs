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
        TaskView GetTask(int id);
        void EditTask(Task task);
        void DeleteTask(Task task);
    }
}
