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
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenderDTO> CreateAsync(GenderDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid");
                }

                var entity = _mapper.Map<Gender>(model);
                entity = _unitOfWork.GendersRepository.Create(entity);
                await _unitOfWork.SaveAsync();
                model = _mapper.Map<GenderDTO>(entity);
                return model;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GendersRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Album doesn't exist");
            }
            _unitOfWork.GendersRepository.Remove(entity);
            return await _unitOfWork.SaveAsync() != 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<GenderDTO> FindAll()
        {
            var enities = _unitOfWork.GendersRepository.GetAll();
            var models = _mapper.Map<IEnumerable<GenderDTO>>(enities);
            return models;
        }

        public async Task<GenderDTO> FindByIdAsync(int id)
        {
            var entity = await _unitOfWork.GendersRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Album doesn't exist");
            }
            var model = _mapper.Map<GenderDTO>(entity);
            return model;
        }

        public async Task<bool> UpdateAsync(GenderDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid");
                }

                var entity = _mapper.Map<Gender>(model);
                _unitOfWork.GendersRepository.Update(entity);
                return await _unitOfWork.SaveAsync() != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
