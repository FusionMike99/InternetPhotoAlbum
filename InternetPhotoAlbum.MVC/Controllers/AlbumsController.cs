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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    /// <summary>
    /// Controlling requests for albums
    /// </summary>
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject album service and mapper
        /// </summary>
        /// <param name="albumService">Album service</param>
        /// <param name="mapper">Mapper</param>
        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        /// <summary>
        /// Process the GET request for index albums
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var albums = _albumService.FindAll();

            ViewData["IsHavePermission"] = false;

            return View(albums);
        }

        /// <summary>
        /// Process the GET request for user's albums
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Route("{userId}")]
        public ActionResult UserAlbums(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.Identity.GetUserId();
            }

            var albums = _albumService.FindByUserId(userId);

            bool isHavePermission = false;

            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    isHavePermission = userIdValue == userId;
                }
            }

            ViewData["IsHavePermission"] = isHavePermission;

            return View("Index", albums);
        }

        /// <summary>
        /// Process the GET request for create album
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        }

        /// <summary>
        /// Process the POST request for create album
        /// </summary>
        /// <param name="model">Album's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dtoModel = _mapper.Map<AlbumDTO>(model);
                    dtoModel.UserId = User.Identity.GetUserId();
                    var result = await _albumService.CreateAsync(dtoModel);
                    return Json(new { url = Url.Action("UserAlbums") });
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
        /// Process the GET request for edit album
        /// </summary>
        /// <param name="id">Album's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    var album = await _albumService.FindByIdAsync(id.Value);
                    if (CheckPermission(album.UserId))
                    {
                        return PartialView(album);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
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
        /// Process the POST request for edit album
        /// </summary>
        /// <param name="model">Album's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AlbumDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _albumService.UpdateAsync(model);
                    return Json(new { url = Url.Action("UserAlbums") });
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
        /// Process the GET request for delete album
        /// </summary>
        /// <param name="id">Album's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    var album = await _albumService.FindByIdAsync(id.Value);
                    if (CheckPermission(album.UserId))
                    {
                        return PartialView(album);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
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
        /// Process the POST request for delete album
        /// </summary>
        /// <param name="id">Album's identifier</param>
        /// <returns>Result of action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _albumService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _albumService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckPermission(string userId)
        {
            var authenticatedUserId = User.Identity.GetUserId();
            return authenticatedUserId == userId;
        }
    }
}