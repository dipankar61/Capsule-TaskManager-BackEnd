using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DataAccess;
using TaskManager.Business;


namespace TaskManager.Api.Controllers
{
    public class TaskManagerController : ApiController
    {
        private readonly ITaskManagerHandler handlerTaskManger;
        private readonly TaskManagerContext context;
        public TaskManagerController()
        {
            this.context = new TaskManagerContext();
            handlerTaskManger = new TaskManagerHandler(new TaskRepository(context));
        }
        public IHttpActionResult GetAllTasks()
        {
            try
            {
                var tasklist = handlerTaskManger.GetAllTask();
                if (tasklist.Count == 0)
                    return NotFound();
                return Ok(tasklist);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                context.Dispose();
            }
        }
        public IHttpActionResult PostNewTask(Task task)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                handlerTaskManger.AddNewTask(task);
                return Ok();
            }
            catch(CustomValidationException ex)
            {
                return ReturnCustomError(ModelState, ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                context.Dispose();
            }
        }
        public IHttpActionResult PutEditTask(Task task)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                handlerTaskManger.EditTask(task);
                return Ok();
            }
            catch (CustomValidationException ex)
            {
                return ReturnCustomError(ModelState, ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                context.Dispose();
            }
        }
        private IHttpActionResult ReturnCustomError(System.Web.Http.ModelBinding.ModelStateDictionary modleState,CustomValidationException customEx)
        {
            foreach (var dicItem in customEx.ErrorCollection)
            {
                modleState.AddModelError(dicItem.Key, dicItem.Value);
            }
            return BadRequest(modleState);

        }
    }
}
