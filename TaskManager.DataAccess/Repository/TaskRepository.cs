using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerContext ctx;
        public TaskRepository(TaskManagerContext ctx)
        {
            this.ctx = ctx;
        }
        public void AddTask(Task task)
        {

            ctx.Tasks.Add(new Task
            {
                TaskName = task.TaskName,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Priority = task.Priority,
                ParentTaskId = task.ParentTaskId


            });
              
                ctx.SaveChanges();
           
        }

        public void DeleteTask(Task task)
        {
            
                ctx.Tasks.Remove(task);
                ctx.SaveChanges();
            
        }

        public void EditTask(Task task)
        {
            
                var editTask = ctx.Tasks.Where(objtask => objtask.TaskId == task.TaskId).Single();
                editTask.TaskName = task.TaskName;
                editTask.StartDate = task.StartDate;
                editTask.EndDate = task.EndDate;
                editTask.Priority = task.Priority;
                editTask.ParentTaskId = task.ParentTaskId;
                ctx.SaveChanges();
           
        }

        public IQueryable<Task> GetAllTask()
        {
            
            return ctx.Tasks;
          
        }
        //public IQueryable<Task> GetAllParentTasks()
        //{
        //    return ctx.Tasks.Include("TasksUnderParents").Where(t => (t.TasksUnderParents.Count > 0 && !t.EndDate.HasValue) || !t.ParentTaskId.HasValue);
        //}

        public Task GetTaskByID(int taskID)
        {
           
             return ctx.Tasks.Where(objtask => objtask.TaskId == taskID).SingleOrDefault();
           
        }

        public Task GetTaskByName(string name)
        {
            
            return ctx.Tasks.Where(objtask => objtask.TaskName == name && (!objtask.EndDate.HasValue || objtask.EndDate.Value>DateTime.Now)).SingleOrDefault();
          
        }
    }
}
