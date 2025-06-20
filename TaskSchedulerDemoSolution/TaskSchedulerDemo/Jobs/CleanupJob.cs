using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class CleanupJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Cleanup job executed at {DateTime.Now}");
            // Add cleanup logic here
            return Task.CompletedTask;
        }
    }
}
