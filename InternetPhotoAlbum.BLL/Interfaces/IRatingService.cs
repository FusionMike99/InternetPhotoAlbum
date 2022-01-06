using InternetPhotoAlbum.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.BLL.Interfaces
{
    /// <summary>
    /// Implementation of Service for Rating
    /// </summary>
    public interface IRatingService : IDisposable
    {
        /// <summary>
        /// Add new rating
        /// </summary>
        /// <param name="model">Rating being added</param>
        /// <returns>Added rating</returns>
        Task<RatingDTO> CreateAsync(RatingDTO model);

        /// <summary>
        /// Find all ratings
        /// </summary>
        /// <returns>List of ratings</returns>
        IEnumerable<RatingDTO> FindAll();

        /// <summary>
        /// Find rating by identifiers
        /// </summary>
        /// <param name="imageId">Image's identifier</param>
        /// <param name="userId">User's identifier</param>
        /// <returns>Found rating</returns>
        Task<RatingDTO> FindByIdAsync(int imageId, string userId);

        /// <summary>
        /// Change rating
        /// </summary>
        /// <param name="model">Updatable rating</param>
        /// <returns>Operation result</returns>
        Task<bool> UpdateAsync(RatingDTO model);

        /// <summary>
        /// Delete rating
        /// </summary>
        /// <param name="imageId">Image's identifier</param>
        /// <param name="userId">User's identifier</param>
        /// <returns>Operation result</returns>
        Task<bool> DeleteAsync(int imageId, string userId);

        /// <summary>
        /// Rate image
        /// </summary>
        /// <param name="model">Data for rating image</param>
        /// <returns>Task</returns>
        Task RateImage(RatingDTO model);

        /// <summary>
        /// Calculate final rating of image
        /// </summary>
        /// <param name="imageId">Image's identifier</param>
        /// <returns>Final rating</returns>
        int CalculateFinalRating(int imageId);
    }
}
