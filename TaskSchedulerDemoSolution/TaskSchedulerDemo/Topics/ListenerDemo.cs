using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TaskSchedulerDemo.Jobs;
using TaskSchedulerDemo.Listeners;

namespace TaskSchedulerDemo.Topics
{
    internal class ListenerDemo
    {
        public static async Task<IScheduler> ScheduleListenerDemo()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            // Add the scheduler listener
            scheduler.ListenerManager.AddSchedulerListener(new HelloSchedulerListener());
            scheduler.ListenerManager.AddJobListener(new HelloJobListener());
            scheduler.ListenerManager.AddTriggerListener(new HelloTriggerListener());

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("helloJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("helloTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            return scheduler;
        }
    }
}
