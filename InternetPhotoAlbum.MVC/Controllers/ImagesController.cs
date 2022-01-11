using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    /// <summary>
    /// Controlling requests for images
    /// </summary>
    public class ImagesController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IImageService _imageService;
        private readonly IRatingService _ratingService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject album, image, rating services and mapper
        /// </summary>
        /// <param name="albumService">Album service</param>
        /// <param name="imageService">Image service</param>
        /// <param name="ratingService">Rating service</param>
        /// <param name="mapper">Mapper</param>
        public ImagesController(IAlbumService albumService, IImageService imageService, IRatingService ratingService, IMapper mapper)
        {
            _albumService = albumService;
            _imageService = imageService;
            _ratingService = ratingService;
            _mapper = mapper;
        }



        /// <summary>
        /// Process the GET request for index images
        /// </summary>
        /// <param name="albumId">Album's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        public async Task<ActionResult> Index(int? albumId)
        {
            if (albumId == null)
            {
                return HttpNotFound();
            }

            var images = _imageService.FindByAlbumId(albumId.Value).ToList();
            var model = _mapper.Map<IEnumerable<IndexImageViewModel>>(images);

            var userId = User.Identity.GetUserId();
            var album = await _albumService.FindByIdAsync(albumId.Value);
            bool isHavePermission = userId == album.UserId;

            ViewData["IsHavePermission"] = isHavePermission;

            TempData["AlbumUserId"] = album.UserId;

            ViewData["AlbumId"] = albumId;

            return View(model);
        }

        /// <summary>
        /// Process the GET request for details image
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                try
                {
                    var userId = User.Identity.GetUserId();

                    var image = await _imageService.FindByIdAsync(id.Value);
                    var model = _mapper.Map<IndexImageViewModel>(image);

                    model.FinalRating = _ratingService.CalculateFinalRating(model.Id);

                    int likeId = 0;
                    RatingDTO ratingDTO = null;

                    try
                    {
                        ratingDTO = await _ratingService.FindByIdAsync(id.Value, userId);
                        likeId = ratingDTO.LikeTypeId;
                    }
                    catch (InvalidOperationException ex) when (ex.Message == "Rating doesn't exist")
                    {
                    }

                    bool isHavePermission = userId == (string)TempData.Peek("AlbumUserId");

                    ViewData["IsHavePermission"] = isHavePermission;

                    ViewData["LikeId"] = likeId;

                    return View(model);
                }
                catch (InvalidOperationException ex)
                {
                    return HttpNotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Process the POST request for search images by title
        /// </summary>
        /// <param name="title">Image's title</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByTitle([Required] string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var image = _imageService.FindByTitle(title);
                var model = _mapper.Map<IEnumerable<IndexImageViewModel>>(image);

                return View("IndexByTitle", model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Process the GET request for create image
        /// </summary>
        /// <param name="albumId">Album's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Create(int? albumId)
        {
            if (albumId == null)
            {
                return HttpNotFound();
            }

            ViewData["AlbumId"] = albumId.Value;
            return PartialView();
        }

        /// <summary>
        /// Process the POST request for create image
        /// </summary>
        /// <param name="model">Image's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dtoModel = _mapper.Map<ImageDTO>(model);
                    var result = await _imageService.CreateAsync(dtoModel);
                    return Json(new { url = Url.Action("Index", new { albumId = model.AlbumId }) });
                }
                catch (AggregateValidationException ex)
                {
                    List<ValidationResult> validationResults = ex.ValidationResults;
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError("", validationResult.ErrorMessage);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["AlbumId"] = model.AlbumId;
            return PartialView(model);
        }

        /// <summary>
        /// Process the GET request for edit image
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    var image = await _imageService.FindByIdAsync(id.Value);
                    var model = _mapper.Map<EditImageViewModel>(image);

                    return PartialView(model);
                }
                catch (InvalidOperationException ex)
                {
                    return HttpNotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Process the POST request for edit image
        /// </summary>
        /// <param name="model">Image's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dtoModel = _mapper.Map<ImageDTO>(model);
                    var result = await _imageService.UpdateAsync(dtoModel);
                    return Json(new { url = Url.Action("Index", new { albumId = model.AlbumId }) });
                }
                catch (AggregateValidationException ex)
                {
                    List<ValidationResult> validationResults = ex.ValidationResults;
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError("", validationResult.ErrorMessage);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return PartialView(model);
        }

        /// <summary>
        /// Process the GET request for delete image
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    var image = await _imageService.FindByIdAsync(id.Value);
                    var model = _mapper.Map<IndexImageViewModel>(image);
                    return PartialView(model);
                }
                catch (InvalidOperationException ex)
                {
                    return HttpNotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Process the POST request for delete image
        /// </summary>
        /// <param name="id">Image's identifier</param>
        /// <returns>Result of action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _imageService.DeleteAsync(id);
            return RedirectToAction("Index", "Albums");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _imageService.Dispose();
                _ratingService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}