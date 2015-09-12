# asp_net_synchronization_context
Experiments regarding synchronisation and execution context flow in asp.net

================================================================

public async Task<JsonResult> IndexAsync()<br />
{<br />
    ...<br />
    Task<long> t1 = AsyncService.IOBoundMethod();<br />
    Task<long> t2 = AsyncService.IOBoundMethod();<br />
    Task<long> t3 = AsyncService.IOBoundMethod();<br />
    await Task.WhenAll(t1, t2, t3);<br />
    ...<br />
}<br /><br />
<br />
2015-09-12 15:30:38,847 [6] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexAsync(): <br />
2015-09-12 15:30:38,895 [6] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 6<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexAsync<br /><br /><br />

 
2015-09-12 15:30:39,638 [6] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 15:30:39,646 [6] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 6<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexAsync<br /><br /><br />

 
2015-09-12 15:30:40,812 [14] INFO   asp_net_synchronization_context.AsyncService - Inside AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 15:30:40,822 [14] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 14<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexAsync<br /><br /><br />

 
2015-09-12 15:30:40,831 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.IndexAsync(), after running async methods: <br />
2015-09-12 15:30:40,838 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 14<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexAsync<br />

<br /><br />
================================================================<br /><br /><br />


public JsonResult IndexAsynchronousOperationCannotBeStarted()<br />
{<br />
    ...<br />
    long t1 = AsyncService.IOBoundMethod().Result;
    ...<br />
}<br /><br />

================================================================<br /><br />

public async Task<JsonResult> IndexDeadlock()<br />
{<br />
    ...<br />
    long t1 = AsyncService.IOBoundMethod().Result;
    ...<br />
}<br /><br />

2015-09-12 17:04:04,581 [12] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexDeadlock(): <br />
2015-09-12 17:04:04,587 [12] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 12<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexDeadlock<br />
<br /><br />
 
2015-09-12 17:04:04,591 [12] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 17:04:04,593 [12] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 12<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexDeadlock<br />
<br /><br />

================================================================<br /><br />

public async Task<JsonResult> IndexConfigureAwaitFalse()<br />
{<br />
    ...<br />
    long t1 = await AsyncService.IOBoundMethod().ConfigureAwait(false);
    ...<br />
}<br /><br />

2015-09-12 15:43:31,890 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexConfigureAwaitFalse(): <br />
2015-09-12 15:43:31,899 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 14<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexConfigureAwaitFalse<br /><br /><br />

 
2015-09-12 15:43:31,903 [14] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 15:43:31,909 [14] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 14<br />
	Thread.CurrentThread.IsBackground: True<br /><br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexConfigureAwaitFalse<br />
<br /><br />
 
2015-09-12 15:43:32,441 [12] INFO   asp_net_synchronization_context.AsyncService - Inside<br /> AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 15:43:32,446 [12] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 12<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexConfigureAwaitFalse<br />
<br /><br />
 
2015-09-12 15:43:32,461 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.IndexConfigureAwaitFalse(), after running async method with ConfigureAwait(false): <br />
2015-09-12 15:43:32,464 [14] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 14<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />

================================================================<br /><br /><br />

public JsonResult IndexTaskOnThreadpool()<br />
{<br />
    ...<br />
    var task = Task.Run(async () => <br />
    {<br />
        _log.Info("Inside async delegate, before calling async method:");<br />
        _log.Info(StateDumper.Dump());<br />
        long r = await AsyncService.IOBoundMethod();<br />
        _log.Info("Inside async delegate, after calling async method:");<br />
        _log.Info(StateDumper.Dump());<br />
        return r;<br />
    }); <br />
    long result = task.Result;<br />
    ...<br />
}<br />

<br /><br />

2015-09-12 16:19:38,519 [13] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexTaskOnThreadpool(): <br />
2015-09-12 16:19:38,524 [13] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 13<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br />
 
2015-09-12 16:19:38,527 [21] INFO   asp_net_synchronization_context.Controllers.HomeController - Inside async delegate, before calling async method: <br />
2015-09-12 16:19:38,530 [21] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 21<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:19:38,533 [21] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 16:19:38,538 [21] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 21<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:19:39,149 [16] INFO   asp_net_synchronization_context.AsyncService - Inside AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 16:19:39,153 [16] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 16<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:19:39,156 [16] INFO   asp_net_synchronization_context.Controllers.HomeController - Inside async delegate, after calling async method: <br />
2015-09-12 16:19:39,160 [16] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 16<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:19:39,163 [13] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.IndexTaskOnThreadpool(), after running async method: <br />
2015-09-12 16:19:39,167 [13] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 13<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br />

================================================================<br /><br /><br />

public async Task<JsonResult> IndexTaskOnThreadpoolAsync()<br />
{<br />
    ...<br />
    long result = await Task.Run(async () => <br />
    {<br />
        _log.Info("Inside async delegate, before calling async method:");<br />
        _log.Info(StateDumper.Dump());<br />
        long r = await AsyncService.IOBoundMethod();<br />
        _log.Info("Inside async delegate, after calling async method:");<br />
        _log.Info(StateDumper.Dump());<br />
        return r;<br />
    });<br />
    ...<br />
}
<br /><br /><br />

2015-09-12 16:28:13,972 [24] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexTaskOnThreadpoolAsync(): <br />
2015-09-12 16:28:13,975 [24] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 24<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br />
 
2015-09-12 16:28:13,982 [24] INFO   asp_net_synchronization_context.Controllers.HomeController - Inside async delegate, before calling async method: <br />
2015-09-12 16:28:13,985 [24] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 24<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:28:13,987 [24] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 16:28:13,988 [24] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 24<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 16:28:14,504 [9] INFO   asp_net_synchronization_context.AsyncService - Inside AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 16:28:14,507 [9] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 9<br /><br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br /><br /><br />

 
2015-09-12 16:28:14,511 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - Inside async delegate, after calling async method: <br />
2015-09-12 16:28:14,514 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 9<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br /><br /><br />

 
2015-09-12 16:28:14,516 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.Index(), after running async method: <br />
2015-09-12 16:28:14,519 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 9<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br /><br />
 
