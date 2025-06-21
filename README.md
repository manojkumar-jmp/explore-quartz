# Sample Project for Quartz.NET Tutorials

A demonstration project showcasing how to schedule jobs (tasks or pieces of work) to run at specified times or intervals, similar to how Windows Task Scheduler or cron jobs work using Quartz.NET an open-source, full-featured job scheduling library for .NET applications.

### Hosting

> 📌 Note: Quartz.NET is a library—it requires a host process to run. Without a host, Quartz.NET has no way to start or stop, nor can it manage job execution during the application’s lifecycle.<br>
The purpose of this project is to explore the Quartz.NET scheduler. To keep things simple, self-hosted (standalone) approach via a Console Application built on .NET Framework 4.8. is used.

There are several options available to host Quartz.NET such as Standalone (Self-Hosted), Microsoft.Extensions.Hosting Integration, ASP.NET Core Integration, Windows Service, Clustered Hosting.

> 💡 I will explore other hosting options in upcoming sample projects.

## Getting Started

### Prerequisites

- Visual Studio 2022
- .NET Framework 4.8.1
- Quartz.NET NuGet package

Installation
1.	Clone the repository:<br>
 `     git clone https://github.com/yourusername/TaskSchedulerDemo.git
  	`
2.	Open the solution in Visual Studio 2022.
3.	Restore NuGet packages.

### How to Run an Example:

1. Open the main entry point (e.g., Program.cs).
2. Uncomment the line for the example you want to run, and comment out all others.
3. Build and run the project.
4. When finished, press any key to exit as prompted in the console.

### Examples Covered

- Hello Scheduler <br>
  Basic usage of Quartz.NET to schedule and run a simple job.
  
```csharp
  IScheduler scheduler = await HelloScheduler.SayHelloScheduler();
```

- Multiple Jobs <br>
  Schedule and run multiple jobs.
  
```csharp
    IScheduler scheduler = await MultipleJobs.ScheduleMultipleJobs();
```
- Passing Data to Jobs <br>
  Pass parameters/data to jobs.
  
```csharp
    IScheduler scheduler = await PassingDataToJobs.ScheduleJobsWithData();
```

- Multiple Triggers <br>
  Use multiple triggers for a single job.

  ```csharp
     IScheduler scheduler = await MultipleTrigger.RunMultipleTriggers();
  ```

  - Listener Demo <br>
   Add listeners to respond to job/trigger events.

  ```csharp
    IScheduler scheduler = await ListenerDemo.ScheduleListenerDemo();
  ```
- Exception Handling <br>
  Handle exceptions in job execution.

  ```csharp
     IScheduler scheduler = await ExceptionHandling.ScheduleExceptionHandlingDemo();
  ```

  - Retrying Job Demo <br>
   Configure a job to retry on failure

   ```csharp
     IScheduler scheduler = await RetryingJobDemo.RetryJobDemo();
  ```

  - Stateful Job Demo <br>
    Demonstrate jobs that maintain state between runs.

    ```csharp
       IScheduler scheduler = await StatefulJobDemo.RunStatefulJobDemo();
    ```
   
  
