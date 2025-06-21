using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskSchedulerDemo.Jobs;

namespace TaskSchedulerDemo.Topics
{
    public class HelloScheduler
    {
        public static async Task<IScheduler> SayHelloScheduler()
        {
            // 1. Create a scheduler factory (manages scheduler instances)
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            // 2. Get a scheduler instance from the factory
            IScheduler scheduler = await schedulerFactory.GetScheduler();

            // 3. Start the scheduler (it will begin executing jobs)
            await scheduler.Start();

            // 4. Define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity(name: "helloJob", group: "group1")
                .Build();

            // 5. Create a trigger that fires now and then every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("helloTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // 6. Tell Quartz to schedule the job using the trigger
            await scheduler.ScheduleJob(job, trigger);

            return scheduler;
        }

    }
}
