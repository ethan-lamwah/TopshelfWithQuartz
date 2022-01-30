using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QuartzSampleApp
{
    /// <summary>
    /// Quartz job scheduling system
    /// </summary>
    class ScheduleService
    {
        private readonly IScheduler _scheduler;

        public ScheduleService()
        {
            // load config from quartz.config
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

            // Add listener
            _scheduler.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());
            _scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
        }

        public void ScheduleJobs()
        {
            // Create a job
            IJobDetail job = JobBuilder.Create<TransactionJob>()
                .WithIdentity("job1", "group1")
                .UsingJobData("TransactionDate", "2022-01-26")
                .UsingJobData("TransactionAccount", "999-123456-001")
                .Build();

            // Creat a trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                //.StartNow()
                .StartAt(DateBuilder.DateOf(6, 0, 0))
                .WithSimpleSchedule(x => x
                    //.WithIntervalInMinutes(1)
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

            // Tell quartz to schedule the job using the trigger
            //await _scheduler.ScheduleJob(job, trigger);
            _scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Start()
        {
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();

            ScheduleJobs();
        }

        public void Stop()
        {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
