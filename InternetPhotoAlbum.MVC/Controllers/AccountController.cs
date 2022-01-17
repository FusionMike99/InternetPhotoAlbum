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
using System.Web.Configuration;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Controllers
{
    /// <summary>
    /// Controlling requests for account
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject gender service and mapper
        /// </summary>
        /// <param name="genderService">Gender service</param>
        /// <param name="mapper">Mapper</param>
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

        /// <summary>
        /// Process the GET request for Log In user
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Process the POST request for Log In user
        /// </summary>
        /// <param name="model">Data for Log In</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claim = await UserService.AuthenticateAsync(model);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Albums");
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

        /// <summary>
        /// Process the GET request for Log Out user
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Albums");
        }

        /// <summary>
        /// Process the GET request for Register user
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        public ActionResult Register()
        {
            var genders = new SelectList(_genderService.FindAll(), "Id", "Name");
            ViewData["Genders"] = genders;

            var minusYears = GetMinUserAge() * (-1);
            ViewData["MaxDateOfBirth"] = DateTime.Now.AddYears(minusYears).ToString("yyyy-MM-dd");

            return View();
        }

        /// <summary>
        /// Process the POST request for Register user
        /// </summary>
        /// <param name="model">Data for registration</param>
        /// <returns>Result of action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserDTO>(model);
                userDto.Role = "user";

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

            var minusYears = GetMinUserAge() * (-1);
            ViewData["MaxDateOfBirth"] = DateTime.Now.AddYears(minusYears).ToString("yyyy-MM-dd");

            return View(model);
        }

        /// <summary>
        /// Process the GET request for index users
        /// </summary>
        /// <returns>Result of action</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = UserService.FindAll();
            var models = _mapper.Map<IEnumerable<IndexUserViewModel>>(users);

            return View(models);
        }

        /// <summary>
        /// Process the GET request for details user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
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

        /// <summary>
        /// Process the GET request for edit user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
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

                    var minusYears = GetMinUserAge() * (-1);
                    ViewData["MaxDateOfBirth"] = DateTime.Now.AddYears(minusYears).ToString("yyyy-MM-dd");

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
        /// Process the POST request for edit user
        /// </summary>
        /// <param name="model">User's model</param>
        /// <returns>Result of action</returns>
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
                    return Json(new { url = Url.Action("Details", new { id = model.Id }) });
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

        /// <summary>
        /// Process the GET request for change user's password
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
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

        /// <summary>
        /// Process the POST request for change user's password
        /// </summary>
        /// <param name="model">Data for changing password</param>
        /// <returns>Result of action</returns>
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

                return Json(new { url = Url.Action("Details", new { id = model.Id }) });
            }

            return PartialView(model);
        }

        /// <summary>
        /// Process the GET request for delete user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
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

        /// <summary>
        /// Process the POST request for delete user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await UserService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Process the GET request for lock user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <returns>Result of action</returns>
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

        /// <summary>
        /// Process the POST request for lock user
        /// </summary>
        /// <param name="id">User's identifier</param>
        /// <param name="isLocked">Unlock or lock user</param>
        /// <returns>Result of action</returns>
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

        private int GetMinUserAge()
        {
            var settings = WebConfigurationManager.AppSettings;
            var minUserAge = Convert.ToInt32(settings["minUserAge"]);
            return minUserAge;
        }
    }
}