using System;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace ActionFilterDemo.Filters
{
    // example based on http://blog.tallan.com/2011/02/04/global-action-filters-in-asp-net-mvc-3/

    public class DebugInfoAttribute : ActionFilterAttribute
    {
        readonly Stopwatch _startWatch = new Stopwatch();
        private static string _outputFormat = 
            "<h4>Debug Environment Info</h4><div class=\"debuginfo\"><table><tr><td>Web Server:</td><td>{0}</td></tr><tr><td>Browser:</td><td>{1}</td></tr><tr><td>Controller</td><td>{2}</td></tr><tr><td>Action:</td><td>{3}</td></tr><tr><td>Execution Time(ms):</td><td>{4}</td></tr></table></div>";
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _startWatch.Reset();
            _startWatch.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _startWatch.Stop();
            var browser = filterContext.HttpContext.Request.Browser;
            filterContext.HttpContext.Response.Write(
                string.Format(_outputFormat,
                      HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                      String.Format("{0} ({1})",browser.Browser,browser.Version),
                      filterContext.RouteData.Values["controller"],
                      filterContext.RouteData.Values["action"],
                      _startWatch.ElapsedMilliseconds)
                );
 
        }
    }
}