using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class ReportJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Report generated at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
