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

            //Simulate transaction server job
            ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-001");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-002");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-003");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-004");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-005");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-006");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-007");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-008");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-009");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-010");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-011");
            //ScheduleJobWithCronSchedule<TransactionJob>("999-1234567-012");
        }

        // using simple schedule
        private void ScheduleJobWithSimpleSchedule<T>(string account) where T : IJob
        {
            string jobName = typeof(T).Name + "." + account;
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .UsingJobData("TransactionDate", "2022-01-28")
                .UsingJobData("TransactionAccount", account)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-Trigger", $"{jobName}-Trigger-Group")
                .StartAt(DateBuilder.DateOf(6, 0, 0))
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    //.WithIntervalInSeconds(10)
                    .RepeatForever())
                .ForJob(job)
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }

        //Using Cron expression
        private void ScheduleJobWithCronSchedule<T>(string account) where T : IJob
        {
            string jobName = typeof(T).Name + "." + account;
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .UsingJobData("TransactionDate", "2022-01-28")
                .UsingJobData("TransactionAccount", account)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}-Trigger", $"{jobName}-Trigger-Group")
                //.StartNow()
                .StartAt(DateBuilder.DateOf(6, 0, 0))
                .WithCronSchedule("0 0/1 6-22 ? * MON,TUE,WED,THU,FRI,SAT")
                .ForJob(job)
                .EndAt(DateBuilder.DateOf(22, 0, 0))
                .Build();

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
