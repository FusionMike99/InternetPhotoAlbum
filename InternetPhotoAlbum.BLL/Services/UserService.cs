using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="mapper">Mapper</param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(LoginModel model)
        {
            ApplicationUser user = await _unitOfWork.UserManager.FindAsync(model.Login, model.Password);
            if (user != null)
            {
                var claim = await _unitOfWork.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                return claim;
            }
            else
            {
                throw new InvalidOperationException($"Invalid login or password");
            }
        }

        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var result = await _unitOfWork.UserManager
                .ChangePasswordAsync(userId, oldPassword, newPassword);

            if (result.Errors.Count() > 0)
            {
                var validationResults = new List<ValidationResult>();
                foreach (var error in result.Errors)
                {
                    validationResults.Add(new ValidationResult(error));
                }
                throw new AggregateValidationException("Error with registration", validationResults);
            }

            return result.Succeeded;
        }

        public async Task<UserDTO> CreateAsync(UserDTO model)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(model.Login);
            if (user == null)
            {
                var validationResults = new List<ValidationResult>();
                var context = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, context, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }
                user = new ApplicationUser { Email = model.Email, UserName = model.Login };
                var result = await _unitOfWork.UserManager.CreateAsync(user, model.Password);
                if (result.Errors.Count() > 0)
                {
                    validationResults = new List<ValidationResult>();
                    foreach (var error in result.Errors)
                    {
                        validationResults.Add(new ValidationResult(error));
                    }
                    throw new AggregateValidationException("Error with registration", validationResults);
                }
                await _unitOfWork.UserManager.AddToRoleAsync(user.Id, model.Role);
                model.Id = user.Id;
                var userProfile = _mapper.Map<UserDTO, UserProfile>(model);
                userProfile = _unitOfWork.UserProfilesRepository.Create(userProfile);
                await _unitOfWork.SaveAsync();
                var userDTO = _mapper.Map<UserProfile, UserDTO>(userProfile);
                return userDTO;
            }
            else
            {
                throw new InvalidOperationException($"User {model.Login} exist");
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var identityResult = await _unitOfWork.UserManager.DeleteAsync(user);
                if (identityResult.Errors.Count() > 0)
                {
                    var validationResults = new List<ValidationResult>();
                    foreach (var error in identityResult.Errors)
                    {
                        validationResults.Add(new ValidationResult(error));
                    }
                    throw new AggregateValidationException("Error with deleting user", validationResults);
                }
                return identityResult.Succeeded;
            }
            else
            {
                throw new InvalidOperationException("User doesn't exist");
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<UserDTO> FindAll()
        {
            var users = _unitOfWork.UserManager.Users.ToList();
            var result = _mapper.Map<IEnumerable<UserDTO>>(users);
            return result;
        }

        public async Task<UserDTO> FindByIdAsync(string id)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var userProfile = await _unitOfWork.UserProfilesRepository.GetByIdAsync(id);
                user.UserProfile = userProfile;
                var result = _mapper.Map<ApplicationUser, UserDTO>(user);
                return result;
            }
            else
            {
                throw new InvalidOperationException("User doesn't exist");
            }
        }

        public async Task LockUser(string userId, bool isLocked)
        {
            var identityResult = await _unitOfWork.UserManager.SetLockoutEnabledAsync(userId, isLocked);
            if (identityResult.Errors.Count() > 0)
            {
                var validationResults = new List<ValidationResult>();
                foreach (var error in identityResult.Errors)
                {
                    validationResults.Add(new ValidationResult(error));
                }
                throw new AggregateValidationException("Error with locking user", validationResults);
            }
            _unitOfWork.ProceduresRepository.LockUser(userId, isLocked);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateAsync(EditUserProfileModel model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var context = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, context, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }
                var entity = _mapper.Map<EditUserProfileModel, UserProfile>(model);
                _unitOfWork.UserProfilesRepository.Update(entity);
                var result = await _unitOfWork.SaveAsync();
                return result != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
