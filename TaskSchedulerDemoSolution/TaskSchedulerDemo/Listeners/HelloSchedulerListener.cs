using Quartz;
using Quartz.Listener;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerDemo.Listeners
{
    // Inherit from SchedulerListenerSupport to provide default (empty) implementations
    public class HelloSchedulerListener : SchedulerListenerSupport
    {
        public override Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[SchedulerListener] Job added: {jobDetail.Key}");
            return Task.CompletedTask;
        }

        public override Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[SchedulerListener] Job deleted: {jobKey}");
            return Task.CompletedTask;
        }

        public override Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[SchedulerListener] Job scheduled: {trigger.JobKey}");
            return Task.CompletedTask;
        }

        public override Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[SchedulerListener] Job unscheduled: {triggerKey}");
            return Task.CompletedTask;
        }

        public override Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("[SchedulerListener] Scheduler started");
            return Task.CompletedTask;
        }

        public override Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("[SchedulerListener] Scheduler shutdown");
            return Task.CompletedTask;
        }
    }
}
