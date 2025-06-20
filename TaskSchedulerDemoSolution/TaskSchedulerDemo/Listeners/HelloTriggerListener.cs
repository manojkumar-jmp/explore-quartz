using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Listener;
using Quartz;
using System.Threading;

namespace TaskSchedulerDemo.Listeners
{
    public class HelloTriggerListener : TriggerListenerSupport
    {
        public override string Name => "LoggingTriggerListener";

        public override Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[TriggerListener] Trigger fired for job: {trigger.JobKey}");
            return Task.CompletedTask;
        }

        public override Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[TriggerListener] Trigger misfired: {trigger.Key}");
            return Task.CompletedTask;
        }

        public override Task TriggerComplete(
            ITrigger trigger,
            IJobExecutionContext context,
            SchedulerInstruction triggerInstructionCode,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[TriggerListener] Trigger completed for job: {trigger.JobKey}");
            return Task.CompletedTask;
        }

        public override Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            // Return true to prevent the job from executing
            return Task.FromResult(false); // Don't veto by default
        }
    }
}
