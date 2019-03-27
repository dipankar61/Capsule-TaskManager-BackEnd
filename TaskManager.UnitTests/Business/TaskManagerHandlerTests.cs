using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Business;
using TaskManager.DataAccess;

namespace TaskManager.UnitTests.Business
{
    [TestFixture]
    public class TaskManagerHandlerTests
    {
        [Test]
        public void EditTask_NoChildTask_Successful_Edit()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            mockTaskRepository.Setup(p => p.GetAllTask()).Returns(childTasks);
            mockTaskRepository.Setup(p => p.EditTask(taskEdit));
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            objTestTaskMangerHandler.EditTask(taskEdit);
            mockTaskRepository.Verify(mock => mock.GetAllTask(), Times.Once());
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());
            mockTaskRepository.Verify(mock => mock.EditTask(taskEdit), Times.Once());

            // ...or verify everything.
            // mockSomeClass.VerifyAll();

        }
        [Test]
       
        public void EditTask_ActiveSameName_Exception_FailedEdit()
        {
            Task task = new Task();
            task.TaskId = 2;
            task.TaskName = "ABCD";
            task.StartDate = DateTime.Now;
            task.Priority = 1;
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
           
            Assert.That(() => objTestTaskMangerHandler.EditTask(taskEdit),
                Throws.TypeOf<CustomValidationException>());
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());

        }
    }
}
