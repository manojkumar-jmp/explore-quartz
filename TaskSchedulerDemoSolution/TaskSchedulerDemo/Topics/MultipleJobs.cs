using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using TaskSchedulerDemo.Jobs;

namespace TaskSchedulerDemo.Topics
{
    internal class MultipleJobs
    {
        // Possible schedule extension methods are:
        // WithSimpleSchedule: For simple repeating schedules.  
        // WithCronSchedule: For cron-like schedules.   
        // WithCalendarIntervalSchedule: For calendar interval schedules.
        // WithDailyTimeIntervalSchedule: For daily time interval schedules.
        // WithCalendar: For using a calendar to exclude certain times.

        public static async Task<IScheduler> ScheduleMultipleJobs()
        {
            var weeklyCalendar = new WeeklyCalendar();
            weeklyCalendar.SetDayExcluded(DayOfWeek.Saturday, true);
            weeklyCalendar.SetDayExcluded(DayOfWeek.Friday, true);

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();            
            IScheduler scheduler = await schedulerFactory.GetScheduler();            
            await scheduler.Start();

            // HelloJob: every 10 seconds (simple trigger) WithSimpleSchedule
            IJobDetail helloJob = JobBuilder.Create<HelloJob>()
                .WithIdentity("helloJob", "group1")
                .Build();
            ITrigger helloTrigger = TriggerBuilder.Create()
                .WithIdentity("helloTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(helloJob, helloTrigger);

            // ReportJob: every minute at the 15th second (cron trigger) WithCronSchedule
            IJobDetail reportJob = JobBuilder.Create<ReportJob>()
                .WithIdentity("reportJob", "group1")
                .Build();
            ITrigger reportTrigger = TriggerBuilder.Create()
                .WithIdentity("reportTrigger", "group1")
                .WithCronSchedule("15 * * * * ?") // At second 15 of every minute
                .Build();
            await scheduler.ScheduleJob(reportJob, reportTrigger);

            // CleanupJob: every day at 2:30 AM (cron trigger)
            IJobDetail cleanupJob = JobBuilder.Create<CleanupJob>()
                .WithIdentity("cleanupJob", "group1")
                .Build();
            ITrigger cleanupTrigger = TriggerBuilder.Create()
                .WithIdentity("cleanupTrigger", "group1")
                .WithCronSchedule("0 30 2 ? * *") // At 2:30 AM every day
                .Build();
            await scheduler.ScheduleJob(cleanupJob, cleanupTrigger);

            await scheduler.AddCalendar("weeklyCalendar", weeklyCalendar, replace: false, updateTriggers: false);
            IJobDetail weeklyCalendarJod = JobBuilder.Create<CalendarJob>()
                .WithIdentity("weeklyCalendarJob", "group1")
                .Build();
            ITrigger weeklyCalendarTrigger = TriggerBuilder.Create()
                .WithIdentity("weeklyCalendarTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .ModifiedByCalendar("weeklyCalendar") // Use the weekly calendar
                .Build();
            await scheduler.ScheduleJob(weeklyCalendarJod, weeklyCalendarTrigger);
            return scheduler;
        }
    }
}
