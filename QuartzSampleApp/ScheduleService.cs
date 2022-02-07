using Quartz;
using Quartz.Impl.Matchers;
using QuartzSampleApp.Jobs;
using QuartzSampleApp.Listener;

namespace QuartzSampleApp
{
    /// <summary>
    /// Quartz job scheduling system
    /// </summary>
    class SchedulerService
    {
        private readonly IScheduler _scheduler;

        public SchedulerService(IScheduler scheduler)
        {
            _scheduler = scheduler;

            // Add listener
            _scheduler.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());
            _scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
        }

        private void ScheduleJobs()
        {
            //JobDataMap jobDataMap = new JobDataMap
            //{
            //    new System.Collections.Generic.KeyValuePair<string, object>("TransactionDate", "2022-01-26"),
            //    new System.Collections.Generic.KeyValuePair<string, object>("TransactionAccount", "999-123456-001")
            //};
            ScheduleJobWithCronSchedule<TransactionJob>("500-259957-001");
            //ScheduleJobWithCronSchedule<TransactionJob>("808-893630-209");
            //ScheduleJobWithCronSchedule<TransactionJob>("500-259957-201");
            //ScheduleJobWithCronSchedule<TransactionJob>("511-509820-002");
            //ScheduleJobWithCronSchedule<TransactionJob>("808-893671-209");
            //ScheduleJobWithCronSchedule<TransactionJob>("808-485866-201");
            //ScheduleJobWithCronSchedule<TransactionJob>("848-369252-001");
            //ScheduleJobWithCronSchedule<TransactionJob>("848-369252-201");
            //ScheduleJobWithCronSchedule<TransactionJob>("741-061295-838");
            //ScheduleJobWithCronSchedule<TransactionJob>("741-061410-201");
            //ScheduleJobWithCronSchedule<TransactionJob>("741-324818-838");
            //ScheduleJobWithCronSchedule<TransactionJob>("741-323984-278");
        }

        private void ScheduleJobWithSimpleSchedule<T>(string account) where T : IJob
        {
            string jobName = typeof(T).Name + "." + account;
            // Create a job
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .UsingJobData("TransactionDate", "2022-01-28")
                .UsingJobData("TransactionAccount", account)
                .Build();

            // Creat a trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-Trigger", $"{jobName}-Trigger-Group")
                //.StartNow()
                .StartAt(DateBuilder.DateOf(6, 0, 0))
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    //.WithIntervalInSeconds(10)
                    .RepeatForever())
                .ForJob(job)
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

            // Schedule the job using the trigger
            _scheduler.ScheduleJob(job, trigger);
        }

        private void ScheduleJobWithCronSchedule<T>(string account) where T : IJob
        {
            string jobName = typeof(T).Name + "." + account;
            // Create a job
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .UsingJobData("TransactionDate", "2022-01-28")
                .UsingJobData("TransactionAccount", account)
                .Build();

            // Creat a trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-Trigger", $"{jobName}-Trigger-Group")
                //.StartNow()
                .StartAt(DateBuilder.DateOf(6, 0, 0))
                .WithCronSchedule("0 0/1 6-22 ? * MON,TUE,WED,THU,FRI,SAT")
                .ForJob(job)
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

            // Schedule the job using the trigger
            _scheduler.ScheduleJob(job, trigger);
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
