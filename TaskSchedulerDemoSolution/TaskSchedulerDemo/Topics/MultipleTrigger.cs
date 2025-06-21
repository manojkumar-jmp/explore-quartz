using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskSchedulerDemo.Jobs;

namespace TaskSchedulerDemo.Topics
{
    public class MultipleTrigger
    {
        public static async Task<IScheduler> RunMultipleTriggers()
        {
            // 1. Create a scheduler factory (manages scheduler instances)
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            // 2. Get a scheduler instance from the factory
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            // 3. Start the scheduler (it will begin executing jobs)
            await scheduler.Start();
            // 4. Define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(name: "notificationJob", group: "group1")
                .UsingJobData("RecipientEmail", "user@example.com") // Job-wide data
                .Build();
            // 5. Create a trigger that fires now and then every 10 seconds
            ITrigger dailyTrigger = TriggerBuilder.Create()
                .WithIdentity("dailyTrigger", "group1")
                .UsingJobData("Message", "Daily Report") // Trigger-specific data
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            // 6. Create another trigger that fires now and then every 15 seconds
            ITrigger weeklyTrigger = TriggerBuilder.Create()
                .WithIdentity("weeklyTrigger", "group1")
                 .UsingJobData("Message", "Weekly Summary") // Trigger-specific data
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(15)
                    .RepeatForever())
                .ForJob("notificationJob", "group1")  // Associate with existing job
                .Build();
            // 7. Tell Quartz to schedule the job using both triggers
            await scheduler.ScheduleJob(job, dailyTrigger);
            await scheduler.ScheduleJob(weeklyTrigger);

            return scheduler;
        }
    }
}
