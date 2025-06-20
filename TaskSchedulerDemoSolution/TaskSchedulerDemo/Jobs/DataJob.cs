using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    internal class DataJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // Retrieve the data passed to the job / trigger
            string jobMessage = context.JobDetail.JobDataMap.GetString("JobMessage");
            string triggerMessage = context.Trigger.JobDataMap.GetString("TriggerMessage");
            string overridableMessageFromJob = context.JobDetail.JobDataMap.GetString("OverridableMessage");
            string overridableMessageFromTrigger = context.Trigger.JobDataMap.GetString("OverridableMessage");

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
