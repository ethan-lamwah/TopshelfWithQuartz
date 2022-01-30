using Quartz;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzSampleApp
{
    public class JobListener : IJobListener
    {
        public string Name { get; } = nameof(JobListener);

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Log.Information("[J-Listener] JobExecutionVetoed: {context.JobDetail.Key}", context.JobDetail.Key);
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string account = dataMap.GetString("TransactionAccount");
            Log.Information("[J-Listener] JobToBeExecuted: {context.JobDetail.Key}, for {account}", context.JobDetail.Key, account);
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            Log.Information("[J-Listener] JobWasExecuted: {context.JobDetail.Key}", context.JobDetail.Key);
            return Task.CompletedTask;
        }
    }

    public class SchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobAdded: {jobDetail.Key}", jobDetail.Key);
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobDeleted: {jobKey}", jobKey);
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobInterrupted: {jobKey}", jobKey);
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobPaused: {jobKey}", jobKey);
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobResumed: {jobKey}", jobKey);
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobScheduled: {trigger.Key}", trigger.Key);
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobsPaused: {jobGroup}", jobGroup);
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobsResumed: {jobGroup}", jobGroup);
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobUnscheduled: {triggerKey}", triggerKey);
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerError: {msg}", msg);
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerInStandbyMode");
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerInStandbyMode");
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerShuttingdown");
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerStarted");
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulerStarting");
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] SchedulingDataCleared");
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] TriggerFinalized: {trigger}", trigger);
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] TriggerPaused: {triggerKey}", triggerKey);
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] TriggerResumed: {triggerKey}", triggerKey);
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] TriggersPaused: {triggerGroup}", triggerGroup);
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] TriggersResumed: {triggerGroup}", triggerGroup);
            return Task.CompletedTask;
        }
    }
}
