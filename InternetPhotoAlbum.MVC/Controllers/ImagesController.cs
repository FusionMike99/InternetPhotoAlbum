using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImagesController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(int? albumId)
        {
            if (albumId == null)
            {
                return HttpNotFound();
            }

            var images = _imageService.FindByAlbumId(albumId.Value);
            var model = _mapper.Map<IEnumerable<IndexImageViewModel>>(images);

            ViewData["AlbumId"] = albumId;

            return View(model);
        }

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
                    return RedirectToAction("Index", new { albumId = model.AlbumId });
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
    }
}