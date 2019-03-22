using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess;

namespace TaskManager.Business
{
    internal class TaskManagerHandler : ITaskManagerHandler
    {
        private readonly ITaskRepository taskRepo;
        internal TaskManagerHandler(ITaskRepository taskRepo)
        {
            this.taskRepo = taskRepo;
        }
        public void AddNewTask(DataAccess.Task task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(DataAccess.Task task)
        {
            throw new NotImplementedException();
        }

        public void EditTask(DataAccess.Task task)
        {
            throw new NotImplementedException();
        }

        public List<DataAccess.Task> GetAllParentTask()
        {
            throw new NotImplementedException();
        }

        public List<TaskView> GetAllTask()
        {
            throw new NotImplementedException();
        }
    }
}
