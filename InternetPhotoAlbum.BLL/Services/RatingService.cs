﻿using AutoMapper;
using InternetPhotoAlbum.BLL.Infrastructure;
using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RatingDTO> CreateAsync(RatingDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<Rating>(model);
                entity = _unitOfWork.RatingsRepository.Create(entity);
                await _unitOfWork.SaveAsync();
                model = _mapper.Map<RatingDTO>(entity);
                return model;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }

        public async Task<bool> DeleteAsync(int imageId, string userId)
        {
            var entity = await _unitOfWork.RatingsRepository.GetByIdAsync(imageId, userId);
            if (entity == null)
            {
                throw new InvalidOperationException("Rating doesn't exist");
            }
            _unitOfWork.RatingsRepository.Remove(entity);
            return await _unitOfWork.SaveAsync() != 0;
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

        public async Task RateImage(RatingDTO model)
        {
            var rating = await _unitOfWork.RatingsRepository.GetByIdAsync(model.ImageId, model.UserId);
            if (rating == null)
            {
                await CreateAsync(model);
            }
            else if (rating.LikeTypeId != model.LikeTypeId)
            {
                await UpdateAsync(model);
            }
            else if(rating.LikeTypeId == model.LikeTypeId)
            {
                await DeleteAsync(model.ImageId, model.UserId);
            }
        }

        public async Task<bool> UpdateAsync(RatingDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<Rating>(model);
                _unitOfWork.RatingsRepository.Update(entity);
                return await _unitOfWork.SaveAsync() != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
