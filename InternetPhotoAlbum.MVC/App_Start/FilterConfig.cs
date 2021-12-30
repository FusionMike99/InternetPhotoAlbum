using InternetPhotoAlbum.MVC.Infrastructure;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggerFilter());
        }
    }
}
