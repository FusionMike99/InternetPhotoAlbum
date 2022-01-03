using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IMapper mapper)
        {
            _genderService = genderService;
            _mapper = mapper;
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claim = await UserService.AuthenticateAsync(model);
                    if (claim == null)
                    {
                        ModelState.AddModelError("Password", "Invalid password");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("UserAlbums", "Albums");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Albums");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var genders = new SelectList(_genderService.FindAll(), "Id", "Name");

            ViewData["Genders"] = genders;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Login = model.Login,
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    GenderId = model.GenderId,
                    Role = "user"
                };

                try
                {
                    var result = await UserService.CreateAsync(userDto);
                    return RedirectToAction("Login");
                }
                catch (AggregateValidationException ex)
                {
                    List<ValidationResult> validationResults = ex.ValidationResults;
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError("", validationResult.ErrorMessage);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var genders = new SelectList(_genderService.FindAll(), "Id", "Name");
            ViewData["Genders"] = genders;

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = UserService.FindAll();
            var models = _mapper.Map<IEnumerable<IndexUserViewModel>>(users);

            return View(models);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Details(string id)
        {
            if (id != null)
            {
                try
                {
                    var user = await UserService.FindByIdAsync(id);
                    var model = _mapper.Map<IndexUserViewModel>(user);

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
        public async Task<ActionResult> Edit(string id)
        {
            if (id != null)
            {
                try
                {
                    var user = await UserService.FindByIdAsync(id);
                    var model = _mapper.Map<EditUserProfileModel>(user);

                    var genders = new SelectList(_genderService.FindAll(), "Id", "Name");

                    ViewData["Genders"] = genders;

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
        public async Task<ActionResult> Edit(EditUserProfileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await UserService.UpdateAsync(model);
                    return RedirectToAction("Details", new { id = model.Id });
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

            var genders = new SelectList(_genderService.FindAll(), "Id", "Name");
            ViewData["Genders"] = genders;

            return PartialView(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword(string id)
        {
            if (id != null)
            {
                var model = new ChangePasswordViewModel
                {
                    Id = id,
                    OldPassword = string.Empty,
                    NewPassword = string.Empty
                };

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.ChangePassword(model.Id, model.OldPassword, model.NewPassword);

                if (!result)
                {
                    ModelState.AddModelError("", "Failed to change password");
                }

                return RedirectToAction("Details", new { id = model.Id });
            }

            return PartialView(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id != null)
            {
                try
                {
                    var user = await UserService.FindByIdAsync(id);
                    var model = _mapper.Map<IndexUserViewModel>(user);

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
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await UserService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> LockUser(string id)
        {
            if (id != null)
            {
                try
                {
                    var user = await UserService.FindByIdAsync(id);
                    var model = _mapper.Map<IndexUserViewModel>(user);

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
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LockUser(string id, bool isLocked)
        {
            await UserService.LockUser(id, !isLocked);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserService.Dispose();
                _genderService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}