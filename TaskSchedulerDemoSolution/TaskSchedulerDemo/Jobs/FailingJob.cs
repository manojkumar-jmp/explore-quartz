using System;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class FailingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Failing job started.");
                await Task.Delay(1000); // Waits asynchronously for 1 second
                // This exception is caught by Quartz, not by main program.
                throw new Exception("This job is intentionally failing.");
            }
            catch (Exception ex)
            {

                throw new JobExecutionException(ex)
                {
                    RefireImmediately = false
                };
            }
        }
    }
}
