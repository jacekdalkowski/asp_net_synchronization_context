using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace asp_net_synchronization_context.Utils
{
    public class StateDumper
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(StateDumper));

        public static string Dump()
        {
            return DumpThread() + DumpSynchronizationContext() + DumpHttpContext() + DumpSession() + "\r\n";
        }

        public static string DumpThread()
        {
            string state = "Thread.CurrentThread.ManagedThreadId: " + Thread.CurrentThread.ManagedThreadId + "\r\n"
                + "\tThread.CurrentThread.IsBackground: " + Thread.CurrentThread.IsBackground + "\r\n"
                + "\tThread.CurrentThread.IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread + "\r\n"
                + "\tThread.CurrentThread.CurrentCulture: " + Thread.CurrentThread.CurrentCulture + "\r\n";
            return state;
        }

        public static string DumpSynchronizationContext()
        {
            string state = "SynchronizationContext.Current: " + (SynchronizationContext.Current != null ? SynchronizationContext.Current.ToString() : "null") + "\r\n";
            return state;
        }

        public static string DumpHttpContext()
        {
            string state = "HttpContext.Current: " + (HttpContext.Current != null ? HttpContext.Current.ToString() : "null") + "\r\n";
            if(HttpContext.Current != null)
            {
                state += "\tHttpContext.Current.Request: " + HttpContext.Current.Request != null ? HttpContext.Current.Request.ToString() : "null" + "\r\n";
            }
            return state;
        }

        public static string DumpSession()
        {
            string session = "HttpContext.Current.Session: " + ((HttpContext.Current != null && HttpContext.Current.Session != null) ? HttpContext.Current.Session.ToString() : "null") + "\r\n";
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                foreach (var sessionKey in HttpContext.Current.Session)
                {
                    session += "\tSession entry: " + sessionKey + " - " + HttpContext.Current.Session[sessionKey.ToString()].ToString() + "\r\n";
                }
            }
            return session;
        }
    }
}