﻿using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                try
                {
                    var image = await _imageService.FindByIdAsync(id.Value);
                    var model = _mapper.Map<IndexImageViewModel>(image);

                    ViewData["AlbumId"] = image.AlbumId;

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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    var image = await _imageService.FindByIdAsync(id.Value);
                    var model = _mapper.Map<CreateImageViewModel>(image);

                    ViewData["AlbumId"] = image.AlbumId;
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createModel = _mapper.Map<CreateImageViewModel>(model);
                    //createModel.File = file;
                    var dtoModel = _mapper.Map<ImageDTO>(createModel);
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

            return PartialView(model);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _imageService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}