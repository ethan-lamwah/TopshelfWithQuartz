using Serilog;
using System;
using System.IO;
using Topshelf;

namespace QuartzSampleApp
{
    /// <summary>
    /// Configure a custom Topshelf service and its logic
    /// </summary>
    internal static class ConfigureTService
    {
        internal static void Configure()
        {
            var rc = HostFactory.Run(configurator =>
            {
                configurator.Service<ScheduleService>(sc =>
                {
                    sc.ConstructUsing(() => new ScheduleService());
                    // the start and stop methods for the service
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.Stop());
                    // optional pause/continue methods if used
                    // optional, when shutdown is supported
                });

                // Run the service using the local system account
                configurator.RunAsLocalSystem();

                // Config serilog
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}logs\\log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, rollOnFileSizeLimit: true, shared: true, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();
                configurator.UseSerilog(Log.Logger);

                // Config basic information
                configurator.SetDescription("My Topshelf service with Quartz.NET and Serilog");
                configurator.SetDisplayName("Topshelf service with Quartz");
                configurator.SetServiceName("Topshelf-Quartz");

                // Service Start Mode
                configurator.StartAutomaticallyDelayed();

                // Service Recovery
                configurator.EnableServiceRecovery(src =>
                {
                    src.RestartService(3);
                });
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
