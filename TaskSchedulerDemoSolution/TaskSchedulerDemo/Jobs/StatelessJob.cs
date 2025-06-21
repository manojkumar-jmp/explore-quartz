using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    public class StatelessJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // Stateless job logic goes here
            Console.WriteLine("Stateless job executed at " + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
