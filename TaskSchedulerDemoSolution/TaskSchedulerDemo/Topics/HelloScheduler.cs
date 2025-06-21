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
            /* Job Definition (IJobDetail):
             *   1. This is a description of the job, not the job object itself.
             *   2. It includes the job’s type, identity (name/group), and any associated data (JobDataMap).
             *   3. You register this definition with the scheduler.
             * Job Instance Lifetime and Threading
             * A new instance is created for each execution (stateless by default).
             *   • Lifetime:
             *      The job instance exists only for the duration of the Execute method call.
             *   • Threading:
             *      Each job instance is executed on a thread from Quartz.NET’s thread pool.
             *   • Concurrency:
             *     By default, multiple instances of the same job class can run concurrently if multiple triggers fire at the same time.
             */
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity(name: "helloJob", group: "group1")
                .Build();

            // 5. Create a trigger that fires now and then every 10 seconds
            /*
             * When a trigger fires, Quartz.NET:
             *  1. Looks up the IJobDetail for the job.
             *  2. Uses reflection to create a new instance of the job class (e.g., new HelloJob()).
             *  3. Injects the IJobExecutionContext (which includes merged data from JobDataMap).
             *  4. Calls the Execute method on the job instance.
             *  By default, a new job instance is created for every execution.
             */
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
