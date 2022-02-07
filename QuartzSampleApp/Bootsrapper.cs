using Autofac;
using Autofac.Extras.Quartz;
using QuartzSampleApp.Jobs;
using Serilog;
using System;
using System.Collections.Specialized;

namespace QuartzSampleApp
{
    public static class Bootsrapper
    {
        public static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}logs\\log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, rollOnFileSizeLimit: true, shared: true, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SchedulerService>();

            var schedulerConfig = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" },

                { "quartz.scheduler.exporter.type", "Quartz.Simpl.RemotingSchedulerExporter, Quartz" },
                { "quartz.scheduler.exporter.port", "555" },
                {"quartz.scheduler.exporter.bindName" , "QuartzScheduler" },
                {"quartz.scheduler.exporter.channelType", "tcp"},
                {"quartz.scheduler.exporter.channelName", "httpQuartz"}
            };

            // 1. Register IScheduler
            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });
            //builder.RegisterModule(new QuartzAutofacFactoryModule());

            // 2. Register jobs
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(TransactionJob).Assembly));

            var container = builder.Build();
            return container;
        }
    }
}
