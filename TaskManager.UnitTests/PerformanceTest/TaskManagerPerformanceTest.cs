using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Api.Controllers;
using TaskManager.DataAccess;
using System.Diagnostics.CodeAnalysis;
using NBench;
//using System.Threading.Tasks;

namespace TaskManager.UnitTests.PerformanceTest
{
    [ExcludeFromCodeCoverage]
    public class TaskManagerPerformanceTest
    {
        private Counter _counter;
        private TaskManagerController _controller;
        private Task _task;
        

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            _controller = new TaskManagerController();
            _task = new Task() { TaskName = "Test1",  Priority = 10, StartDate = System.DateTime.Now, EndDate = System.DateTime.Now.AddDays(7) };
            
        }

        [PerfBenchmark(Description = "Add task through put test.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1200, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void AddTask()
        {
            _controller.PostNewTask(_task);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get All task.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1200, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetSpecificTask()
        {
            _controller.GetAllTasks(false);
            _counter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {

        }
    }
}

