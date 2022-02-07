using Quartz;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzSampleApp.Listener
{
    public class SchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Log.Information("[S-Listener] JobAdded: {0}", jobDetail.Key);
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
            Log.Information("[S-Listener] JobScheduled: {0}", trigger.Key);
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
