# Sample Project for Quartz.NET Tutorials

A demonstration project showcasing how to schedule jobs (tasks or pieces of work) to run at specified times or intervals, similar to how Windows Task Scheduler or cron jobs work using Quartz.NET an open-source, full-featured job scheduling library for .NET applications.

## Hosting

> 📌 Note: Quartz.NET is a library—it requires a host process to run. Without a host, Quartz.NET has no way to start or stop, nor can it manage job execution during the application’s lifecycle.<br>
The purpose of this project is to explore the Quartz.NET scheduler. To keep things simple, self-hosted (standalone) approach via a Console Application built on .NET Framework 4.8. is used.

There are several options available to host Quartz.NET Standalone (Self-Hosted), Microsoft.Extensions.Hosting Integration, ASP.NET Core Integration, Windows Service, Clustered Hosting.

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
