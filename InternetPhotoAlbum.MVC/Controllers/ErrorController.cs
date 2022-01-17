using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    /// <summary>
    /// Controlling application errors
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Error with status code 404 (Not found)
        /// </summary>
        /// <returns>Result of action</returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Error with status code 403 (Forbidden)
        /// </summary>
        /// <returns>Result of action</returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        /// <summary>
        /// Error with status code 400 (Bad request)
        /// </summary>
        /// <returns>Result of action</returns>
        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }
    }
}