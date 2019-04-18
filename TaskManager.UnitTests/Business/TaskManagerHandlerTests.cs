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
        [Test]
        public void EditTask_ChildTask_startEndDate_failedValidation()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.EndDate = DateTime.Now.AddDays(5);
            taskEdit.Priority = 1;
            Task taskchild = new Task();
            taskchild.TaskId = 1;
            taskchild.TaskName = "BCAD";
            taskchild.StartDate = DateTime.Now.AddDays(-2);
            taskchild.EndDate = DateTime.Now.AddDays(8);
            taskchild.ParentTaskId = 1;
            taskchild.Priority = 1;

            List<Task> ctask = new List<Task>();
            ctask.Add(taskEdit);
            ctask.Add(taskchild);
            IQueryable<Task> childTasks = ctask.AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            mockTaskRepository.Setup(p => p.GetAllTask()).Returns(childTasks);
            mockTaskRepository.Setup(p => p.EditTask(taskEdit));
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            Assert.That(() => objTestTaskMangerHandler.EditTask(taskEdit),
             Throws.TypeOf<CustomValidationException>());
            //mockTaskRepository.Verify(mock => mock.GetAllTask(), Times.Once());
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());
            

        }
        [Test]
        public void EditTask_ChildTask_startEndDate_PassValidation()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.EndDate = DateTime.Now.AddDays(5);
            taskEdit.Priority = 1;
            Task taskchild = new Task();
            taskchild.TaskId = 1;
            taskchild.TaskName = "BCAD";
            taskchild.StartDate = DateTime.Now.AddDays(1);
            taskchild.EndDate = DateTime.Now.AddDays(3);
            taskchild.ParentTaskId = 1;
            taskchild.Priority = 1;

            List<Task> ctask = new List<Task>();
            ctask.Add(taskEdit);
            ctask.Add(taskchild);
            IQueryable<Task> childTasks = ctask.AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            mockTaskRepository.Setup(p => p.GetAllTask()).Returns(childTasks);
            mockTaskRepository.Setup(p => p.EditTask(taskEdit));
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            objTestTaskMangerHandler.EditTask(taskEdit);
            mockTaskRepository.Verify(mock => mock.GetAllTask(), Times.Once());
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());
            mockTaskRepository.Verify(mock => mock.EditTask(taskEdit), Times.Once());


        }
        [Test]
        public void AddNewTask_Successful()
        {
            Task task = new Task();
            Task taskAdd = new Task();
            taskAdd.TaskId = 1;
            taskAdd.TaskName = "ABCD";
            taskAdd.StartDate = DateTime.Now;
            taskAdd.Priority = 1;
            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            mockTaskRepository.Setup(p => p.AddTask(taskAdd));
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            objTestTaskMangerHandler.AddNewTask(taskAdd);
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());
            mockTaskRepository.Verify(mock => mock.AddTask(taskAdd), Times.Once());

            // ...or verify everything.
            // mockSomeClass.VerifyAll();

        }
        [Test]
        public void AddNewTask_ActiveSameName_Exception_FailedAdd()
        {
            Task task = new Task();
            task.TaskId = 2;
            task.TaskName = "ABCD";
            task.StartDate = DateTime.Now;
            task.Priority = 1;
            Task taskAdd = new Task();
            taskAdd.TaskId = 1;
            taskAdd.TaskName = "ABCD";
            taskAdd.StartDate = DateTime.Now;
            taskAdd.Priority = 1;
            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            mockTaskRepository.Setup(p => p.GetTaskByName("ABCD")).Returns(task);
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);

            Assert.That(() => objTestTaskMangerHandler.AddNewTask(taskAdd),
                Throws.TypeOf<CustomValidationException>());
            mockTaskRepository.Verify(mock => mock.GetTaskByName("ABCD"), Times.Once());

        }
        [Test]
        public void Delete_Successful()
        {
            Task task = new Task();
            Task taskdel = new Task();
            taskdel.TaskId = 1;
            taskdel.TaskName = "ABCD";
            taskdel.StartDate = DateTime.Now;
            taskdel.Priority = 1;
            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            
            mockTaskRepository.Setup(p => p.DeleteTask(taskdel));
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            objTestTaskMangerHandler.DeleteTask(taskdel);
            mockTaskRepository.Verify(mock => mock.DeleteTask(taskdel), Times.Once());

           
        }
        [Test]
        public void GetAllParentTask_ReturnTasks()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now.AddDays(-3);
            taskEdit.EndDate = DateTime.Now.AddDays(5);
            taskEdit.Priority = 1;
            Task taskchild = new Task();
            taskchild.TaskId = 1;
            taskchild.TaskName = "BCAD";
            taskchild.StartDate = DateTime.Now.AddDays(-2);
            taskchild.EndDate = DateTime.Now.AddDays(-1);
            taskchild.ParentTaskId = 1;
            taskchild.Priority = 1;

            List<Task> ctask = new List<Task>();
            ctask.Add(taskEdit);
            ctask.Add(taskchild);
            IQueryable<Task> childTasks = ctask.AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);
            
            mockTaskRepository.Setup(p => p.GetAllTask()).Returns(childTasks);
            
            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            var pTasks=objTestTaskMangerHandler.GetAllParentTask();
            Assert.AreEqual(pTasks.Count, 1);
           
            mockTaskRepository.Verify(mock => mock.GetAllTask(), Times.Once());
          


        }
        [Test]
        public void GetAllTask_ReturnTasks()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.EndDate = DateTime.Now.AddDays(5);
            taskEdit.Priority = 1;
            Task taskchild = new Task();
            taskchild.TaskId = 2;
            taskchild.TaskName = "BCAD";
            taskchild.StartDate = DateTime.Now.AddDays(1);
            taskchild.EndDate = DateTime.Now.AddDays(3);
            taskchild.ParentTaskId = 1;
            taskchild.Priority = 1;

            List<Task> ctask = new List<Task>();
            ctask.Add(taskEdit);
            ctask.Add(taskchild);
            IQueryable<Task> childTasks = ctask.AsQueryable();

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);

            mockTaskRepository.Setup(p => p.GetAllTask()).Returns(childTasks);
            mockTaskRepository.Setup(p => p.GetTaskByID(1)).Returns(taskEdit);

            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            var pTasks = objTestTaskMangerHandler.GetAllTask();
            Assert.AreEqual(pTasks.Count, 2);

            mockTaskRepository.Verify(mock => mock.GetAllTask(), Times.Once());



        }
        [Test]
        public void GetTaskByID_ReturnTask()
        {
            Task task = new Task();
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.EndDate = DateTime.Now.AddDays(5);
            taskEdit.Priority = 1;
       

            var mockTaskRepository = new Mock<ITaskRepository>(MockBehavior.Strict);

            
            mockTaskRepository.Setup(p => p.GetTaskByID(1)).Returns(taskEdit);

            var objTestTaskMangerHandler = new TaskManagerHandler(mockTaskRepository.Object);
            var pTask = objTestTaskMangerHandler.GetTask(1);
            Assert.AreEqual(pTask.TaskId, 1);
            Assert.AreEqual(pTask.TaskName, "ABCD");
            mockTaskRepository.Verify(mock => mock.GetTaskByID(1), Times.Once());



        }
       
    }
}
