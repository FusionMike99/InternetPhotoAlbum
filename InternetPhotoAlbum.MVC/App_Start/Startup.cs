using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.MVC.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(InternetPhotoAlbum.MVC.App_Start.Startup))]
namespace InternetPhotoAlbum.MVC.App_Start
{
    public class Startup
    {
        private IKernel kernel = null;
        public void Configuration(IAppBuilder app)
        {
            kernel = CreateKernel();
            app.UseNinject(() => kernel);

            app.CreatePerOwinContext(() => kernel.Get<IUserService>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout")
            });
        }

        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["InternetPhotoAlbumDb"].ConnectionString;
                DataAccessBinding dataAccessBinding = new DataAccessBinding(connectionString);
                BusinessLogicBinding businessLogicBinding = new BusinessLogicBinding();
                AutoMapperModule autoMapperModule = new AutoMapperModule();
                kernel.Load(dataAccessBinding, businessLogicBinding, autoMapperModule);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}