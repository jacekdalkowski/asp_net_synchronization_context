using asp_net_synchronization_context.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace asp_net_synchronization_context.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]
    public class HomeController : Controller
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));
        public const string SessionKeyCurrentMethod = "method";

        public async Task<JsonResult> IndexAsync()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexAsync");

            _log.Info("Entering HomeController.IndexAsync():");
            _log.Info(StateDumper.Dump());

            Task<long> t1 = AsyncService.IOBoundMethod();
            Task<long> t2 = AsyncService.IOBoundMethod();
            Task<long> t3 = AsyncService.IOBoundMethod();
            await Task.WhenAll(t1, t2, t3);

            return Json(new { message = "Home/Index"}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexAsynchronousOperationCannotBeStarted()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexAsynchronousOperationCannotBeStarted");

            _log.Info("Entering HomeController.Index():");
            _log.Info(StateDumper.Dump());

            long t1 = AsyncService.IOBoundMethod().Result;

            return Json(new { message = "Home/IndexAsynchronousOperationCannotBeStarted" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> IndexDeadlock()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexDeadlock");

            _log.Info("Entering HomeController.IndexDeadlock():");
            _log.Info(StateDumper.Dump());

            long t1 = AsyncService.IOBoundMethod().Result;

            return Json(new { message = "Home/IndexAsynchronousOperationCannotBeStarted" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> IndexConfigureAwaitFalse()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexConfigureAwaitFalse");

            _log.Info("Entering HomeController.IndexConfigureAwaitFalse():");
            _log.Info(StateDumper.Dump());

            long t1 = await AsyncService.IOBoundMethod().ConfigureAwait(false);

            _log.Info("In HomeController.IndexConfigureAwaitFalse(), after running async method with ConfigureAwait(false):");
            _log.Info(StateDumper.Dump());

            return Json(new { message = "Home/IndexAsynchronousOperationCannotBeStarted" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexTaskOnThreadpool()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexTaskOnThreadpool");

            _log.Info("Entering HomeController.IndexTaskOnThreadpool():");
            _log.Info(StateDumper.Dump());

            var task = Task.Run(async () => await AsyncService.IOBoundMethod()); 
            long result = task.Result;

            _log.Info("In HomeController.Index(), after running async method:");
            _log.Info(StateDumper.Dump());

            return Json(new { message = "Home/IndexTaskOnThreadpool" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> IndexTaskOnThreadpoolAsync()
        {
            SetThreadCulture();
            SaveMethodNameInSession("IndexTaskOnThreadpool");

            _log.Info("Entering HomeController.IndexTaskOnThreadpoolAsync():");
            _log.Info(StateDumper.Dump());

            long result = await Task.Run(async () => await AsyncService.IOBoundMethod());

            _log.Info("In HomeController.Index(), after running async method:");
            _log.Info(StateDumper.Dump());

            return Json(new { message = "Home/IndexTaskOnThreadpoolAsync" }, JsonRequestBehavior.AllowGet);
        }

        private void SetThreadCulture()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
        }

        private void SaveMethodNameInSession(string callerActionName, [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            // callerMethodName always have value of "MoveNext" when async methods are used!
            //Session[SessionKeyCurrentMethod] = callerMemberName;
            Session[SessionKeyCurrentMethod] = callerActionName;
        }
    }
}
