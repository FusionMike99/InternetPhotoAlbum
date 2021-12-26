using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InternetPhotoAlbum.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /*string connectionString = ConfigurationManager.ConnectionStrings["InternetPhotoAlbumDb"].ConnectionString;
            DataAccessBinding dataAccessBinding = new DataAccessBinding(connectionString);
            var kernel = new StandardKernel(dataAccessBinding);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));*/
        }
    }
}
