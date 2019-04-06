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
                bool isStartDateValidationPass = true;
                bool isEndDateValidationPass = true;
                var childTasks = GetChildTasks(task);
                if (childTasks.Count > 0)
                {
                   isStartDateValidationPass = IsEditedParentStartDateValid(childTasks, task);
                   isEndDateValidationPass = IsEditedParentEndDateValid(childTasks, task);
                }
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

        public List<TaskView> GetAllParentTask()
        {
            var lstParentTask = new List<TaskView>();
            var parentTasks = taskRepo.GetAllTask().Where(t=>!t.EndDate.HasValue || t.EndDate.Value>DateTime.Now).ToList();
            //var parentTasks= taskRepo.GetAllTask().Where(objTask => taskRepo.GetAllTask().Any(ptask=>ptask.ParentTaskId== objTask.TaskId) && !objTask.EndDate.HasValue).ToList();
            parentTasks.ToList().ForEach(objTask =>
            {
                var taskview = new TaskView();
                taskview.TaskId = objTask.TaskId;
                taskview.TaskName = objTask.TaskName;
                taskview.StartDate = objTask.StartDate;
                taskview.EndDate = objTask.EndDate;
                taskview.Priority = objTask.Priority;
                taskview.ParentTaskId = objTask.ParentTaskId;
                lstParentTask.Add(taskview);
            }
            );
            return lstParentTask;
        }

        public List<TaskView> GetAllTask()
        {
            var lstTask = new List<TaskView>();
            var allTasks = taskRepo.GetAllTask().ToList();
            allTasks.ToList().ForEach(objTask => {
                var taskview = new TaskView();
                taskview.TaskId = objTask.TaskId;
                taskview.TaskName = objTask.TaskName;
                taskview.StartDate = objTask.StartDate;
                taskview.EndDate = objTask.EndDate;
                taskview.Priority = objTask.Priority;
                taskview.ParentTaskId = objTask.ParentTaskId;
                taskview.ParentTaskName = objTask.ParentTaskId.HasValue ? taskRepo.GetTaskByID(objTask.ParentTaskId.Value).TaskName : null;
                lstTask.Add(taskview);
            }
            );
            return lstTask;
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
            return childTasks.All(t => !task.EndDate.HasValue ||(t.EndDate.HasValue && t.EndDate < task.EndDate));
        }
        
    }
}
