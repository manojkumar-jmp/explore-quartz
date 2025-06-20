using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskSchedulerDemo.Jobs;

namespace TaskSchedulerDemo.Topics
{
    internal class PassingDataToJobs
    {
        public static async Task<IScheduler> ScheduleJobsWithData()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();
            // Define a job that accepts data
            IJobDetail dataJob = JobBuilder.Create<DataJob>()
                .WithIdentity("dataJob", "group1")
                .UsingJobData("JobMessage", "Hello from the job level!")
                .UsingJobData("OverridableMessage", "Overridable message from the job level!")
                .Build();
            // Create a trigger that fires now and then every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("dataTrigger", "group1")
                .UsingJobData("TriggerMessage", "Hello from the trigger level!")
                .UsingJobData("OverridableMessage", "Overridable message from the trigger level!")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();
            // Schedule the job using the trigger
            await scheduler.ScheduleJob(dataJob, trigger);
            return scheduler;
        }
    }
}
