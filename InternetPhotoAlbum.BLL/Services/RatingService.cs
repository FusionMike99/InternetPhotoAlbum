using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Services
{
    /// <inheritdoc cref="IRatingService"/>
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="mapper">Mapper</param>
        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int CalculateFinalRating(int imageId)
        {
            var ratings = _unitOfWork.RatingsRepository.Get(r => r.ImageId == imageId);
            var result = ratings
                .GroupBy(g => g.ImageId)
                .Select(g => g.Count(r => r.LikeTypeId == 1) - g.Count(r => r.LikeTypeId == 2))
                .SingleOrDefault();

            return result;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<RatingDTO> FindAll()
        {
            var enities = _unitOfWork.RatingsRepository.GetAll();
            var models = _mapper.Map<IEnumerable<RatingDTO>>(enities);
            return models;
        }

        public async Task<RatingDTO> FindByIdAsync(int imageId, string userId)
        {
            var entity = await _unitOfWork.RatingsRepository.GetByIdAsync(imageId, userId);
            if (entity == null)
            {
                throw new InvalidOperationException("Rating doesn't exist");
            }
            var model = _mapper.Map<RatingDTO>(entity);
            return model;
        }

        public async Task<bool> RateImage(RatingDTO model)
        {
            ValidateModel(model);

            var rating = await _unitOfWork.RatingsRepository.GetByIdAsync(model.ImageId, model.UserId);
            if (rating == null)
            {
                rating = _mapper.Map<Rating>(model);
                _unitOfWork.RatingsRepository.Create(rating);
            }
            else if (rating.LikeTypeId != model.LikeTypeId)
            {
                rating = _mapper.Map<Rating>(model);
                _unitOfWork.RatingsRepository.Update(rating);
            }
            else if (rating.LikeTypeId == model.LikeTypeId)
            {
                _unitOfWork.RatingsRepository.Remove(rating);
            }

            return await _unitOfWork.SaveAsync() != 0;
        }

        private void ValidateModel(RatingDTO model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                throw new AggregateValidationException("Some properties don't valid", validationResults);
            }
        }
    }
}
