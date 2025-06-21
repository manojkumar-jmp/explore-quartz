using System;
using System.Threading.Tasks;
using Quartz;

namespace TaskSchedulerDemo.Jobs
{

    /// <summary>
    /// 1. Quartz.NET always creates a new instance of your job class for each execution, 
    ///    regardless of whether [DisallowConcurrentExecution] is present.
    /// 2. The [DisallowConcurrentExecution] attribute does not affect instance creation.
    ///    It only ensures that no two executions of the same job(by key) run at the same time.
    /// 3. Without the attribute, if a trigger fires while a previous execution is still running, 
    ///    Quartz will create another instance and run them concurrently.
    /// 4. With the attribute, if a trigger fires while a previous execution is still running, 
    ///    Quartz will wait until the previous execution finishes before starting a new one (but still with a new instance).
    /// </summary>
    [DisallowConcurrentExecution]
    public class StatefulJob: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(10000); // Simulate work
        }
    }
}
