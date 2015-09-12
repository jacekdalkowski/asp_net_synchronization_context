using asp_net_synchronization_context.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace asp_net_synchronization_context
{
    public class AsyncService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AsyncService));

        public static async Task<long> ImmediateMethod()
        {
            return 1;
        }

        public static async Task<long> IOBoundMethod()
        {
            _log.Info("Entering AsyncService.IOBoundMethod():");
            _log.Info(StateDumper.Dump());

            WebClient webClient = new WebClient();
            byte[] webPageData = await webClient.DownloadDataTaskAsync("http://www.wp.pl");

            _log.Info("Inside AsyncService.IOBoundMethod(), after async resource download:");
            _log.Info(StateDumper.Dump());

            return webPageData.Length;
        }

        public static async Task<long> ComputeBoundMethod()
        {
            double sum = 0;
            for(long i = 0; i < long.MaxValue; ++i)
            {
                sum += i;
            }

            return (long)sum;
        }
    }
}