using NLog;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace InternetPhotoAlbum.MVC.Infrastructure
{
    public class LoggerFilter : IActionFilter
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var logInfo = GetInfo(filterContext.RouteData.Values);
            _logger.Info(logInfo);
        }

        private string GetInfo(RouteValueDictionary routeDictionary)
        {
            var stringBuilder = new StringBuilder(string.Empty);
            foreach (var item in routeDictionary)
            {
                stringBuilder.Append(item + "/");
            }

            return stringBuilder.ToString();
        }
    }
}