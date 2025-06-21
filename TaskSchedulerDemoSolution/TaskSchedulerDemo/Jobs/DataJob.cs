using System;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class DataJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            /*
             * The JobDataMap can be used to hold any number of (serializable) objects which you wish to have made available to the job instance when it executes.
             */

            // Retrieve the data passed to the job / trigger
            string jobMessage = context.JobDetail.JobDataMap.GetString("JobMessage");
            string triggerMessage = context.Trigger.JobDataMap.GetString("TriggerMessage");
            string overridableMessageFromJob = context.JobDetail.JobDataMap.GetString("OverridableMessage");
            string overridableMessageFromTrigger = context.Trigger.JobDataMap.GetString("OverridableMessage");

            /* note: use context.MergedJobDataMap in production code
             * It is a merger of the JobDataMap found on the JobDetail and the one found on the Trigger, 
             * with the values of the trigger overriding the same-named values in the job.
             */
            var data = context.MergedJobDataMap;
            string jobMessageFromMergedJobDataMap = data.GetString("JobMessage");
            string triggerMessageFromMergedJobDataMap = data.GetString("TriggerMessage");
            string overridableMessageFromMergedJobDataMap = data.GetString("OverridableMessage");

            Console.WriteLine($"Job Data JobMessage: {jobMessage}, OverridableMessage {overridableMessageFromJob} at {DateTime.Now}");
            Console.WriteLine($"Trigger Data TriggerMessage: {triggerMessage}, OverridableMessage {overridableMessageFromTrigger} at {DateTime.Now}");
            Console.WriteLine($"MergedJobDataMap JobMessage: {jobMessageFromMergedJobDataMap}, TriggerMessage {triggerMessageFromMergedJobDataMap}, OverridableMessage {overridableMessageFromMergedJobDataMap} at {DateTime.Now}");
            return Task.CompletedTask;
        } 
    }
}
