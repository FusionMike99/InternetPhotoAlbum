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
    /// <inheritdoc cref="IImageService"/>
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="mapper">Mapper</param>
        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ImageDTO> CreateAsync(ImageDTO model)
        {
            if (model != null)
            {
                model.AddedDate = DateTime.Now;
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<Image>(model);
                entity = _unitOfWork.ImagesRepository.Create(entity);
                _unitOfWork.ProceduresRepository.UpdateAlbumPeriods(entity.AlbumId);
                await _unitOfWork.SaveAsync();
                model = _mapper.Map<ImageDTO>(entity);
                return model;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.ImagesRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Image doesn't exist");
            }
            _unitOfWork.ImagesRepository.Remove(entity);
            _unitOfWork.ProceduresRepository.UpdateAlbumPeriods(entity.AlbumId);
            return await _unitOfWork.SaveAsync() != 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<ImageDTO> FindAll()
        {
            var enities = _unitOfWork.ImagesRepository.GetAll();
            var models = _mapper.Map<IEnumerable<ImageDTO>>(enities);
            return models;
        }

        public IEnumerable<ImageDTO> FindByAlbumId(int albumId)
        {
            var enities = _unitOfWork.ImagesRepository.Get(a => a.AlbumId == albumId);
            var models = _mapper.Map<IEnumerable<ImageDTO>>(enities);
            return models;
        }

        public async Task<ImageDTO> FindByIdAsync(int id)
        {
            var entity = await _unitOfWork.ImagesRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Image doesn't exist");
            }
            var model = _mapper.Map<ImageDTO>(entity);
            return model;
        }

        public IEnumerable<ImageDTO> FindByTitle(string title)
        {
            title = title.ToLower();
            var enities = _unitOfWork.ImagesRepository.Get(a => a.Title.ToLower().Contains(title));
            var models = _mapper.Map<IEnumerable<ImageDTO>>(enities);
            return models;
        }

        public async Task<bool> UpdateAsync(ImageDTO model)
        {
            if (model != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    throw new AggregateValidationException("Some properties don't valid", validationResults);
                }

                var entity = _mapper.Map<Image>(model);
                _unitOfWork.ImagesRepository.Update(entity);
                return await _unitOfWork.SaveAsync() != 0;
            }
            else
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
