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
    /// <inheritdoc cref="IAlbumService"/>
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="mapper">Mapper</param>
        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AlbumDTO> CreateAsync(AlbumDTO model)
        {
            if (model != null)
            {
                model.PeriodStart = model.PeriodEnd = DateTime.Now;

                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<AlbumDTO, Album>(model);
                entity.User = null;
                entity = _unitOfWork.AlbumsRepository.Create(entity);
                await _unitOfWork.SaveAsync();
                model = _mapper.Map<Album, AlbumDTO>(entity);
                return model;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.AlbumsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Album doesn't exist");
            }
            _unitOfWork.AlbumsRepository.Remove(entity);
            return await _unitOfWork.SaveAsync() != 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<AlbumDTO> FindAll()
        {
            var enities = _unitOfWork.AlbumsRepository.GetAll();
            var models = _mapper.Map<IEnumerable<AlbumDTO>>(enities);
            return models;
        }

        public async Task<AlbumDTO> FindByIdAsync(int id)
        {
            var entity = await _unitOfWork.AlbumsRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Album doesn't exist");
            }
            var model = _mapper.Map<AlbumDTO>(entity);
            return model;
        }

        public IEnumerable<AlbumDTO> FindByUserId(string userId)
        {
            var enities = _unitOfWork.AlbumsRepository.Get(a => a.UserId == userId);
            var models = _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(enities);
            return models;
        }

        public async Task<bool> UpdateAsync(AlbumDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<AlbumDTO, Album>(model);
                entity.User = null;
                _unitOfWork.AlbumsRepository.Update(entity);
                return await _unitOfWork.SaveAsync() != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
