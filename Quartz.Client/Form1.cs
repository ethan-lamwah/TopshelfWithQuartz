using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quartz.Client
{
    public partial class Form1 : Form
    {
        private static IScheduler _scheduler = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            InitRemoteScheduler();
        }

        public static async Task InitRemoteScheduler()
        {
            try
            {
                NameValueCollection properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "schedMaintenanceService";
                properties["quartz.scheduler.proxy"] = "true";
                properties["quartz.scheduler.proxy.address"] = string.Format("{0}://{1}:{2}/QuartzScheduler", "tcp", "localhost", 555);
                ISchedulerFactory sf = new StdSchedulerFactory(properties);

                _scheduler = await sf.GetScheduler();
                Console.WriteLine("GetScheduler success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetScheduler fail" + ex.StackTrace);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            var jobDetail = _scheduler.GetJobDetail(new JobKey("job1", "group1")).GetAwaiter().GetResult();
            Console.WriteLine(jobDetail.JobDataMap["TransactionAccount"]);
            _scheduler.PauseJob(new JobKey("job1", "group1"));
        }

        public static void PauseJob(string jobKey)
        {
            try
            {
                JobKey jk = new JobKey(jobKey);
                if (_scheduler.CheckExists(jk).Result)
                {
                    _scheduler.PauseJob(jk);
                    Console.WriteLine(string.Format("Job {0} is paused", jobKey));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            _scheduler.ResumeJob(new JobKey("job1", "group1"));
        }
    }
}
