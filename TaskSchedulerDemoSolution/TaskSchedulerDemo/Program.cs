using System;
using System.Threading.Tasks;
using Quartz;
using TaskSchedulerDemo.Topics;

namespace TaskSchedulerDemo
{

    internal class Program
    {
        static async Task Main(string[] args)
        {

            // IScheduler scheduler = await HelloScheduler.SayHelloScheduler();
            // IScheduler scheduler = await MultipleJobs.ScheduleMultipleJobs();
            // IScheduler scheduler = await PassingDataToJobs.ScheduleJobsWithData();
            // IScheduler scheduler = await MultipleTrigger.RunMultipleTriggers();
            // IScheduler scheduler = await ListenerDemo.ScheduleListenerDemo();
            // IScheduler scheduler = await ExceptionHandling.ScheduleExceptionHandlingDemo();
            IScheduler scheduler = await RetryingJobDemo.RetryJobDemo(); // Need to revisit 

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();

            // If you pass false, the scheduler will stop immediately, possibly interrupting running jobs
            if (!scheduler.IsShutdown)
                await scheduler.Shutdown(waitForJobsToComplete: true);
        }
    }
}
