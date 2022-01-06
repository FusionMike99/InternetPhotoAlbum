using AutoMapper;
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
    /// <inheritdoc cref="ILikeTypeService"/>
    public class LikeTypeService : ILikeTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="mapper">Mapper</param>
        public LikeTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LikeTypeDTO> CreateAsync(LikeTypeDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<LikeTypeDTO, LikeType>(model);
                entity = _unitOfWork.LikeTypesRepository.Create(entity);
                await _unitOfWork.SaveAsync();
                model = _mapper.Map<LikeType, LikeTypeDTO>(entity);
                return model;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.LikeTypesRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Type of like doesn't exist");
            }
            _unitOfWork.LikeTypesRepository.Remove(entity);
            return await _unitOfWork.SaveAsync() != 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<LikeTypeDTO> FindAll()
        {
            var enities = _unitOfWork.LikeTypesRepository.GetAll();
            var models = _mapper.Map<IEnumerable<LikeTypeDTO>>(enities);
            return models;
        }

        public async Task<LikeTypeDTO> FindByIdAsync(int id)
        {
            var entity = await _unitOfWork.LikeTypesRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Type of like doesn't exist");
            }
            var model = _mapper.Map<LikeTypeDTO>(entity);
            return model;
        }

        public async Task<bool> UpdateAsync(LikeTypeDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<LikeTypeDTO, LikeType>(model);
                _unitOfWork.LikeTypesRepository.Update(entity);
                return await _unitOfWork.SaveAsync() != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
