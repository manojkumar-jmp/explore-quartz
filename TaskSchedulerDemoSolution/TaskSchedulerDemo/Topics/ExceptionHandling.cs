using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using TaskSchedulerDemo.Listeners;

namespace TaskSchedulerDemo.Topics
{
    internal class ExceptionHandling
    {
        public static async Task<IScheduler> ScheduleExceptionHandlingDemo()
        {
            // Create a scheduler factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            // Get a scheduler instance
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            // Start the scheduler
            await scheduler.Start();

            var listener = new GlobalJobListener();
            scheduler.ListenerManager.AddJobListener(listener, EverythingMatcher<JobKey>.AllJobs());
            // Define a job and tie it to the FailingJob class
            IJobDetail job = JobBuilder.Create<Jobs.FailingJob>()
                .WithIdentity("failingJob", "group1")
                .Build();
            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("failingTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                .Build();
            // Schedule the job using the trigger
            await scheduler.ScheduleJob(job, trigger);
            return scheduler;
        }
    }
}
