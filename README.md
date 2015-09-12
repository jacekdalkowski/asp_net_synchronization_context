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
    var task = Task.Run(async () => await AsyncService.IOBoundMethod()); 
    long result = task.Result;
    ...<br />
}<br />

<br /><br />

2015-09-12 15:52:12,967 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexTaskOnThreadpool(): <br />
2015-09-12 15:52:12,975 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 15<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br />
 
2015-09-12 15:52:13,003 [6] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 15:52:13,009 [6] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 6<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 15:52:13,625 [9] INFO   asp_net_synchronization_context.AsyncService - Inside AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 15:52:13,629 [9] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 9<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 15:52:13,640 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.Index(), after running async method: <br />
2015-09-12 15:52:13,649 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 15<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />

================================================================<br /><br /><br />

public async Task<JsonResult> IndexTaskOnThreadpoolAsync()<br />
{<br />
    ...<br />
    long result = await Task.Run(async () => await AsyncService.IOBoundMethod());
    ...<br />
}
<br /><br /><br />

2015-09-12 15:56:00,311 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - Entering HomeController.IndexTaskOnThreadpoolAsync(): <br />
2015-09-12 15:56:00,315 [9] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 9<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />
<br /><br />
 
2015-09-12 15:56:00,321 [15] INFO   asp_net_synchronization_context.AsyncService - Entering AsyncService.IOBoundMethod(): <br />
2015-09-12 15:56:00,323 [15] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 15<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 15:56:00,817 [15] INFO   asp_net_synchronization_context.AsyncService - Inside AsyncService.IOBoundMethod(), after async resource download: <br />
2015-09-12 15:56:00,823 [15] INFO   asp_net_synchronization_context.AsyncService - Thread.CurrentThread.ManagedThreadId: 15<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: en-US<br />
SynchronizationContext.Current: null<br />
HttpContext.Current: null<br />
HttpContext.Current.Session: null<br />
<br /><br />
 
2015-09-12 15:56:00,825 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - In HomeController.Index(), after running async method: <br />
2015-09-12 15:56:00,829 [15] INFO   asp_net_synchronization_context.Controllers.HomeController - Thread.CurrentThread.ManagedThreadId: 15<br />
	Thread.CurrentThread.IsBackground: True<br />
	Thread.CurrentThread.IsThreadPoolThread: True<br />
	Thread.CurrentThread.CurrentCulture: pl-PL<br />
SynchronizationContext.Current: System.Web.AspNetSynchronizationContext<br />
HttpContext.Current: System.Web.HttpContext<br />
System.Web.HttpRequestHttpContext.Current.Session: System.Web.SessionState.HttpSessionState<br />
	Session entry: method - IndexTaskOnThreadpool<br />

