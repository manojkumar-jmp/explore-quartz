using System;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{
    public class NotificationJob: IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string recipientEmail = context.JobDetail.JobDataMap.GetString("RecipientEmail");
            string message = context.Trigger.JobDataMap.GetString("Message");

            Console.WriteLine($"RecipientEmail: {recipientEmail} Message: {message} Notification Job executed at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
