﻿using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(int? imageId, int? likeId)
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
                await _ratingService.CreateAsync(model);
                return RedirectToAction("Details", "Images", new { id = imageId });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(int? imageId, int? likeId)
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
                await _ratingService.UpdateAsync(model);
                return RedirectToAction("Details", "Images", new { id = imageId });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Delete(int? imageId)
        {
            if (imageId != null)
            {
                var userId = User.Identity.GetUserId();
                await _ratingService.DeleteAsync(imageId.Value, userId);
                return RedirectToAction("Details", "Images", new { id = imageId });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

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
    }
}