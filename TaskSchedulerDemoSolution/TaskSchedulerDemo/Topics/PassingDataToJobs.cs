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
            /*
             * Triggers can also have JobDataMaps associated with them. 
             * This can be useful in the case where you have a Job that is stored in the scheduler for regular/repeated use by multiple Triggers, 
             * yet with each independent triggering, you want to supply the Job with different data inputs.
             */
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
