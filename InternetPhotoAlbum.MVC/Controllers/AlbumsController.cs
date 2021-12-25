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
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var albums = _albumService.FindAll();

            ViewData["IsHavePermission"] = false;

            return View(albums);
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult UserAlbums(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                userId = User.Identity.GetUserId();
            }

            var albums = _albumService.FindByUserId(userId);

            bool isHavePermission = false;

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
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

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        }

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
                    return RedirectToAction("UserAlbums");
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var album = await _albumService.FindByIdAsync(id);
                return PartialView(album);
            }
            catch(InvalidOperationException ex)
            {
                return HttpNotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

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
                    return RedirectToAction("UserAlbums");
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var album = await _albumService.FindByIdAsync(id);
                return PartialView(album);
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
    }
}