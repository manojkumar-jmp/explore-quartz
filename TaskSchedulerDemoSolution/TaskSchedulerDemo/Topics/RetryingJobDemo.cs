using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskSchedulerDemo.Jobs;
using TaskSchedulerDemo.Listeners;

namespace TaskSchedulerDemo.Topics
{
    internal class RetryingJobDemo
    {
        public static async Task<IScheduler> RetryJobDemo()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<RetryingJob>()
                .WithIdentity("retryingJob", "group1")                
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("retryingTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .WithRepeatCount(0))
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            return scheduler;
        }
    }
}
