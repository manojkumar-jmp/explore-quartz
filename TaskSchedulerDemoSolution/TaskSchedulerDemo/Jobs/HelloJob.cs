using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Hello, Scheduler! at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
