using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class RetryingJob: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.Trigger.JobDataMap;
            if (!jobDataMap.ContainsKey("RetryCount"))
            {
                jobDataMap.Put("RetryCount", 0); // Initialize retry count if not present
            }
            int retryCount = jobDataMap.GetInt("RetryCount");           
            try
            {

                Console.WriteLine("RetryingJob is executing...");
                // Simulate some work
                await Task.Delay(1000);
                throw new Exception("Simulated exception in RetryingJob");
            }
            catch (Exception ex)
            {
                if(retryCount < 3)
                {
                    Console.WriteLine($"RetryingJob has been retried {retryCount} times.");
                    jobDataMap.Put("RetryCount", retryCount+1);
                    throw new JobExecutionException(ex) { RefireImmediately = true };
                }
                else
                {
                    Console.WriteLine("RetryingJob has reached the maximum retry limit. Failing the job.");
                    throw new JobExecutionException(ex, false); // Do not re-throw to prevent further retries
                }
            }

        }
    } 
}
