using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess
{
    class TaskRepository : ITaskRepository
    {
        public void AddTask(Task task)
        {
            using (var ctx = new TaskManagerContext())
            {
                ctx.Tasks.Add(new Task
                {
                    TaskName = task.TaskName,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    Priority = task.Priority,
                    ParentTaskId = task.ParentTaskId


                }
              );
                ctx.SaveChanges();
            }
        }

        public void DeleteTask(Task task)
        {
            using (var ctx = new TaskManagerContext())
            {
                ctx.Tasks.Remove(task);
                ctx.SaveChanges();
            }
        }

        public void EditTask(Task task)
        {
            using (var ctx = new TaskManagerContext())
            {
                var editTask = ctx.Tasks.Where(objtask => objtask.TaskId == task.TaskId).Single();
                editTask.TaskName = task.TaskName;
                editTask.StartDate = task.StartDate;
                editTask.EndDate = task.EndDate;
                editTask.Priority = task.Priority;
                editTask.ParentTaskId = task.ParentTaskId;
                ctx.SaveChanges();
            }
        }

        public IQueryable<Task> GetAllTask()
        {
            IQueryable<Task> listTask;
            using (var ctx = new TaskManagerContext())
            {
                listTask = ctx.Tasks;
            }
            return listTask;
        }

        public Task GetTaskByID(int taskID)
        {
            Task taskByID;
            using (var ctx = new TaskManagerContext())
            {
                taskByID = ctx.Tasks.Where(objtask => objtask.TaskId == taskID).SingleOrDefault();
            }
            return taskByID;
        }

        public Task GetTaskByName(string name)
        {
            Task taskByName;
            using (var ctx = new TaskManagerContext())
            {
                taskByName = ctx.Tasks.Where(objtask => objtask.TaskName == name).SingleOrDefault();
            }
            return taskByName;
        }
    }
}
