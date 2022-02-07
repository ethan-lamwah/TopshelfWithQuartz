using Quartz;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzSampleApp.Listener
{
    public class JobListener : IJobListener
    {
        public string Name { get; } = nameof(JobListener);

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Log.Information("[J-Listener] JobExecutionVetoed: {0}", context.JobDetail.Key);
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string account = dataMap.GetString("TransactionAccount");
            Log.Information("[J-Listener] JobToBeExecuted: {0}, for {account}", context.JobDetail.Key, account);
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            Log.Information("[J-Listener] JobWasExecuted: {0}", context.JobDetail.Key);
            return Task.CompletedTask;
        }
    }
}
