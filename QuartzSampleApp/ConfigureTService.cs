using Autofac;
using Serilog;
using System;
using Topshelf;
using Topshelf.Autofac;

namespace QuartzSampleApp
{
    /// <summary>
    /// Configure a custom Topshelf service and its logic
    /// </summary>
    internal static class ConfigureTService
    {
        internal static void Configure()
        {
            Bootsrapper.InitializeLogger();
            var container = Bootsrapper.BuildContainer();

            var rc = HostFactory.Run(c =>
            {
                c.UseAutofacContainer(container);
                c.Service<SchedulerService>(s =>
                {
                    //s.ConstructUsing(() => new SchedulerService());
                    s.ConstructUsingAutofacContainer();
                    // the start and stop methods for the service
                    s.WhenStarted(sc => sc.Start());
                    s.WhenStopped(sc => sc.Stop());
                    // optional pause/continue methods if used
                    // optional, when shutdown is supported
                });

                // Run the service using the local system account
                c.RunAsLocalSystem();

                // Config serilog
                c.UseSerilog(Log.Logger);

                // Config basic information
                c.SetDescription("My Topshelf service with Quartz.NET and Serilog");
                c.SetDisplayName("Topshelf service with Quartz");
                c.SetServiceName("Topshelf-Quartz");

                // Service Start Mode
                c.StartAutomaticallyDelayed();

                // Service Recovery
                c.EnableServiceRecovery(src =>
                {
                    src.RestartService(3);
                });
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
