using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TaskManager.DataAccess;


namespace TaskManager.UnitTests.DataAccess
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        [Test]
        public void AddTask_Successful()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            objTestTaskrepo.AddTask(taskEdit);


            mockTaskMangerContext.Verify(mock => mock.SaveChanges(), Times.Once());



        }
        [Test]
        public void DeleteTask_Successful()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            objTestTaskrepo.DeleteTask(taskEdit);


            mockTaskMangerContext.Verify(mock => mock.SaveChanges(), Times.Once());



        }
        [Test]
        public void Edit_Successful()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            objTestTaskrepo.EditTask(taskEdit);


            mockTaskMangerContext.Verify(mock => mock.SaveChanges(), Times.Once());



        }
        [Test]
        public void GetTask_Test()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            var retTask=objTestTaskrepo.GetAllTask();
            Assert.AreEqual(retTask.ToList().Count, 1);
            Assert.AreEqual(retTask.Where(ts=>ts.TaskId== taskEdit.TaskId).ToList().Count, 1);






        }
        [Test]
        public void GetTaskById_Test()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            var retTask = objTestTaskrepo.GetTaskByID(1);
            Assert.AreEqual(retTask.TaskName, taskEdit.TaskName);
           
        }
        [Test]
        public void GetTaskByName_Test()
        {
            Task taskEdit = new Task();
            taskEdit.TaskId = 1;
            taskEdit.TaskName = "ABCD";
            taskEdit.StartDate = DateTime.Now;
            taskEdit.Priority = 1;
            var tasks = new List<Task>();
            tasks.Add(taskEdit);
            var t = tasks.AsQueryable();
            //var mockSet = new Mock<DbSet<Task_Table>>();
            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(t.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(t.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(t.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(t.GetEnumerator());


            IQueryable<Task> childTasks = Enumerable.Empty<Task>().AsQueryable();

            var mockTaskMangerContext = new Mock<TaskManagerContext>();
            mockTaskMangerContext.Setup(c => c.Tasks).Returns(mockSet.Object);
            mockTaskMangerContext.Setup(p => p.SaveChanges());
            var objTestTaskrepo = new TaskRepository(mockTaskMangerContext.Object);
            var retTask = objTestTaskrepo.GetTaskByName("ABCD");
            Assert.AreEqual(retTask.TaskName, "ABCD");

        }
    }

}
