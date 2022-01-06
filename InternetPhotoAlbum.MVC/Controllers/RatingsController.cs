using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    /// <summary>
    /// Controlling requests for ratings
    /// </summary>
    public class RatingsController : Controller
    {
        private readonly IRatingService _ratingService;

        /// <summary>
        /// Inject rating service
        /// </summary>
        /// <param name="ratingService">Rating service</param>
        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Process the POST request for rate image
        /// </summary>
        /// <param name="imageId">Image's identifier</param>
        /// <param name="likeId">Type of like's identifier</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RateImage(int? imageId, int? likeId)
        {
            if (!(imageId == null || likeId == null))
            {
                var userId = User.Identity.GetUserId();
                var model = new RatingDTO
                {
                    ImageId = imageId.Value,
                    UserId = userId,
                    LikeTypeId = likeId.Value,
                };
                await _ratingService.RateImage(model);
                return RedirectToAction("Details", "Images", new { id = imageId });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ratingService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}