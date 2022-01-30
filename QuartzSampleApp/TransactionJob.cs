using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzSampleApp
{
    [DisallowConcurrentExecution]
    public class TransactionJob : IJob
    {
        public string TransactionDate { private get; set; }
        public string TransactionAccount { private get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;
            Log.Warning("Instance {key} of TransactionJob at {TransactionDate}, Account={TransactionAccount}, Previous run: {lastRun}", key, TransactionDate, TransactionAccount, lastRun);
            return Task.CompletedTask;
        }

        //public async Task Execute(IJobExecutionContext context)
        //{
        //    JobKey key = context.JobDetail.Key;

        //    await Console.Out.WriteLineAsync($"[{context.FireTimeUtc.ToLocalTime()}] Instance {key} of CallTransactionAPI at {TransactionDate}, account={TransactionAccount}");
        //}
    }
}
