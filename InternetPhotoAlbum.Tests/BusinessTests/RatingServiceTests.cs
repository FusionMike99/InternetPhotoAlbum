using InternetPhotoAlbum.BLL.Interfaces;
using InternetPhotoAlbum.BLL.Models;
using InternetPhotoAlbum.BLL.Services;
using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.Tests.BusinessTests
{
    public class RatingServiceTests
    {
        [Test]
        public void RatingService_FindAll_ReturnsRatingDTOs()
        {
            // Arrange
            var expected = GetTestRatingDTOs().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetAll())
                .Returns(GetTestRatingEntities().AsQueryable);
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = service.FindAll().ToList();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new RatingDTOEqualityComparer()));
        }

        [Test]
        public async Task RatingService_FindByIdAsync_ReturnsRatingDTO()
        {
            // Arrange
            var expected = GetTestRatingDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(GetTestRatingEntities().First);
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;
            var userId = "1";

            // Act
            var actual = await service.FindByIdAsync(imageId, userId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new RatingDTOEqualityComparer()));
        }

        [Test]
        public void LikeTypeService_FindByIdAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestRatingDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((Rating)null);
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;
            var userId = "1";

            // Act
            Task actual() => service.FindByIdAsync(imageId, userId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [TestCase(1, 2)]
        [TestCase(2, -2)]
        public void RatingService_CalculateFinalRating_ReturnsInteger(int imageId, int expected)
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.Get(It.IsAny<Func<Rating, bool>>()))
                .Returns(GetTestRatingEntities().Where(r => r.ImageId == imageId));
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = service.CalculateFinalRating(imageId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task RatingService_RateImage_AddsModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((Rating)null);
            mockUnitOfWork.Setup(x => x.RatingsRepository.Create(It.IsAny<Rating>()));
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var rating = new RatingDTO
            {
                ImageId = 3,
                UserId = "3",
                LikeTypeId = 1
            };

            //Act
            await service.RateImage(rating);

            //Assert
            mockUnitOfWork.Verify(x => x.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mockUnitOfWork.Verify(x => x.RatingsRepository.Create(It.Is<Rating>(r => r.ImageId == rating.ImageId && r.UserId == rating.UserId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task RatingService_RateImage_UpdatesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(GetTestRatingEntities().First);
            mockUnitOfWork.Setup(x => x.RatingsRepository.Update(It.IsAny<Rating>()));
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var rating = new RatingDTO
            {
                ImageId = 1,
                UserId = "1",
                LikeTypeId = 2
            };

            //Act
            await service.RateImage(rating);

            //Assert
            mockUnitOfWork.Verify(x => x.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mockUnitOfWork.Verify(x => x.RatingsRepository.Update(It.Is<Rating>(r => r.ImageId == rating.ImageId && r.UserId == rating.UserId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task RatingService_RateImage_DeletesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(GetTestRatingEntities().First);
            mockUnitOfWork.Setup(x => x.RatingsRepository.Remove(It.IsAny<Rating>()));
            IRatingService service = new RatingService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var rating = new RatingDTO
            {
                ImageId = 1,
                UserId = "1",
                LikeTypeId = 1
            };

            //Act
            await service.RateImage(rating);

            //Assert
            mockUnitOfWork.Verify(x => x.RatingsRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mockUnitOfWork.Verify(x => x.RatingsRepository.Remove(It.Is<Rating>(r => r.ImageId == rating.ImageId && r.UserId == rating.UserId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        private IEnumerable<RatingDTO> GetTestRatingDTOs()
        {
            return new List<RatingDTO>()
            {
                new RatingDTO { ImageId = 1, UserId = "1", LikeTypeId = 1 },
                new RatingDTO { ImageId = 1, UserId = "2", LikeTypeId = 1 },
                new RatingDTO { ImageId = 2, UserId = "1", LikeTypeId = 2 },
                new RatingDTO { ImageId = 2, UserId = "2", LikeTypeId = 2 }
            };
        }

        private List<Rating> GetTestRatingEntities()
        {
            return new List<Rating>()
            {
                new Rating { ImageId = 1, UserId = "1", LikeTypeId = 1 },
                new Rating { ImageId = 1, UserId = "2", LikeTypeId = 1 },
                new Rating { ImageId = 2, UserId = "1", LikeTypeId = 2 },
                new Rating { ImageId = 2, UserId = "2", LikeTypeId = 2 }
            };
        }
    }
}
