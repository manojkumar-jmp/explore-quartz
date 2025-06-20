using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Listeners
{
    public class GlobalJobListener : IJobListener
    {
        public string Name => "GlobalJobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[GlobalJobListener] Job about to execute: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            if (jobException != null)
                Console.WriteLine($"[GlobalJobListener] Job {context.JobDetail.Key} failed: {jobException.Message}");
            else
                Console.WriteLine($"[GlobalJobListener] Job completed: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            return Task.CompletedTask;
        }
    }
}
