using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    /// <summary>
    /// HelloJob is a simple job that prints a message to the console. 
    /// Each (and every) time the scheduler executes the job, it creates a new instance of the class before calling its Execute(..)
    /// </summary>
    public class HelloJob : IJob
    {
        /* Note: Jobs must have a no-argument constructor
                 It does not make sense to have data-fields defined on the job class - as their values would not be preserved between job executions.
        */

        /// <summary>
        /// Executes the job when triggered by the scheduler.
        /// Method is invoked by one of the scheduler's worker threads.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
            Console.WriteLine($"Instance: {jobKey} Hello, Scheduler! at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
