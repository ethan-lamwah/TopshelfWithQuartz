using Quartz;
using Serilog;
using System.Threading.Tasks;

namespace QuartzSampleApp.Jobs
{
    [DisallowConcurrentExecution]
    public class TransactionJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            string TransactionDate = context.JobDetail.JobDataMap.GetString("TransactionDate");
            string TransactionAccount = context.JobDetail.JobDataMap.GetString("TransactionAccount");
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;

            Log.Warning("Instance {key} of TransactionJob at {TransactionDate}, Account={TransactionAccount}, Previous run: {lastRun}", key, TransactionDate, TransactionAccount, lastRun);
            return Task.CompletedTask;
        }
    }
}
