﻿using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class GendersController : Controller
    {
        private readonly IGenderService _genderService;

        /// <summary>
        /// Inject gender service
        /// </summary>
        /// <param name="genderService">Gender service</param>
        public GendersController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        /// <summary>
        /// Process the GET request for index genders
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var genders = _genderService.FindAll();

            return View(genders);
        }

        /// <summary>
        /// Process the GET request for create gender
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        /// <summary>
        /// Process the POST request for create gender
        /// </summary>
        /// <param name="model">Gender's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GenderDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _genderService.CreateAsync(model);
                    return Json(new { url = Url.Action("Index") });
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
        /// Process the GET request for edit gender
        /// </summary>
        /// <param name="id">Gender's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    var album = await _genderService.FindByIdAsync(id.Value);
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
            else
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Process the POST request for edit gender
        /// </summary>
        /// <param name="model">Gender's model</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GenderDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _genderService.UpdateAsync(model);
                    return Json(new { url = Url.Action("Index") });
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
        /// Process the GET request for delete gender
        /// </summary>
        /// <param name="id">Gender's identifier</param>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    var album = await _genderService.FindByIdAsync(id.Value);
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
            else
            {
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Process the POST request for delete gender
        /// </summary>
        /// <param name="id">Gender's identifier</param>
        /// <returns>Result of action</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _genderService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _genderService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}