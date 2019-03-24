using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess;

namespace TaskManager.Business
{
    public class TaskManagerHandler : ITaskManagerHandler
    {
        private readonly ITaskRepository taskRepo;

        public TaskManagerHandler(ITaskRepository taskRepo)
        {
            this.taskRepo = taskRepo;
        }
        public void AddNewTask(DataAccess.Task task)
        {
            var taskWithSameName = taskRepo.GetTaskByName(task.TaskName);
            if (taskWithSameName !=null && taskWithSameName.TaskId>0)
            {
                var customEx = new CustomValidationException();
                customEx.AddException("TaskName", "Active task with same name is already present in the system");
                throw customEx;
            }
            else
            {              
                taskRepo.AddTask(task);
            }


        }

        public void DeleteTask(DataAccess.Task task)
        {
            taskRepo.DeleteTask(task);
        }

        public void EditTask(DataAccess.Task task)
        {
            var taskWithSameName = taskRepo.GetTaskByName(task.TaskName);
            if (taskWithSameName != null && taskWithSameName.TaskId > 0 && taskWithSameName.TaskId !=task.TaskId)
            {
                var customEx = new CustomValidationException();
                customEx.AddException("TaskName", "Other active task with same name is already present in the system");
                throw customEx;
            }
            else
            {
                var childTasks = GetChildTasks(task);
                var isStartDateValidationPass = IsEditedParentStartDateValid(childTasks, task);
                var isEndDateValidationPass = IsEditedParentEndDateValid(childTasks, task);
                if (childTasks.Count==0 || (isStartDateValidationPass && isEndDateValidationPass))
                    taskRepo.EditTask(task);
                else
                {
                    var customEx = new CustomValidationException();
                    
                    if (!isStartDateValidationPass)
                    {
                        customEx.AddException("StartDate", "One or more child task/tasks has/have earlier start date");
                    }
                    if (!isEndDateValidationPass)
                    {
                        customEx.AddException("EndDate", "One or more child task/tasks has/have greater end date");
                       
                    }
                    throw customEx;

                }
            }
        }

        public List<DataAccess.Task> GetAllParentTask()
        {
            return taskRepo.GetAllTask().Where(objTask => taskRepo.GetAllTask().Any(ptask=>ptask.ParentTaskId== objTask.TaskId) && !objTask.EndDate.HasValue).ToList();
        }

        public List<DataAccess.Task> GetAllTask()
        {
            return taskRepo.GetAllTask().ToList();
        }
        private List<DataAccess.Task> GetChildTasks(DataAccess.Task task)
        {
            return taskRepo.GetAllTask().Where(objTask => objTask.ParentTaskId == task.TaskId).ToList();
            
        }
        private bool IsEditedParentStartDateValid(List<DataAccess.Task> childTasks, DataAccess.Task task)
        {
            return childTasks.All(t => t.StartDate > task.StartDate);
        }
        private bool IsEditedParentEndDateValid(List<DataAccess.Task> childTasks, DataAccess.Task task)
        {
            return childTasks.All(t => t.EndDate < task.EndDate);
        }
        
    }
}
