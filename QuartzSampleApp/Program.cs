using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Logging;

namespace QuartzSampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureTService.Configure();
        }
    }
}
