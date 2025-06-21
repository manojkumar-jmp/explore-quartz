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
        private const int DEFAULT_MAX_RETRIES = 3;
        private const string RETRY_COUNT_KEY = "RetryCount";
        private const string MAX_RETRIES_KEY = "MaxRetries";
        public async Task Execute(IJobExecutionContext context)
        {
            /*
             * Quartz provides a JobDataMap to store and share data between job executions. 
             * Here, it’s used to keep track of how many times the job has been retried.
             */
            var jobDataMap = context.Trigger.JobDataMap;
            if (!jobDataMap.ContainsKey(RETRY_COUNT_KEY))
            {
                jobDataMap.Put(RETRY_COUNT_KEY, 0);
            }

            int maxRetries = jobDataMap.ContainsKey(MAX_RETRIES_KEY)
                ? jobDataMap.GetInt(MAX_RETRIES_KEY)
                : DEFAULT_MAX_RETRIES;

            int retryCount = jobDataMap.GetInt(RETRY_COUNT_KEY);
            try
            {
                Console.WriteLine("RetryingJob is executing...");
                // Simulate some work
                await Task.Delay(1000);
                throw new Exception("Simulated exception in RetryingJob");
            }
            catch (Exception ex)
            {
                if (retryCount < maxRetries)
                {
                    Console.WriteLine($"RetryingJob has been retried {retryCount} times.");
                    jobDataMap.Put(RETRY_COUNT_KEY, retryCount + 1);
                    throw new JobExecutionException(ex) { RefireImmediately = true };
                }
                else
                {
                    Console.WriteLine($"RetryingJob has reached the maximum retry limit of {maxRetries}. Failing the job.");
                    throw new JobExecutionException(ex, false);
                }
            }

        }
    } 
}
