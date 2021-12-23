using InternetPhotoAlbum.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(InternetPhotoAlbum.MVC.App_Start.Startup))]
namespace InternetPhotoAlbum.MVC.App_Start
{
    public class Startup
    {
        private IKernel kernel = null;
        public void Configuration(IAppBuilder app)
        {
            kernel = NinjectWebCommon.CreateKernel();
            app.UseNinject(() => kernel);

            app.CreatePerOwinContext(() => kernel.Get<IUserService>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout")
            });
        }
    }
}