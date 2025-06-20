using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Listener;
using Quartz;
using System.Threading;

namespace TaskSchedulerDemo.Listeners
{
    public class HelloJobListener : JobListenerSupport
    {
        public override string Name => "LoggingJobListener";

        public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[JobListener] Job about to execute: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }

        public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            if (jobException != null)
                Console.WriteLine($"[JobListener] Job {context.JobDetail.Key} failed: {jobException.Message}");
            else
                Console.WriteLine($"[JobListener] Job completed: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }
    }
}
