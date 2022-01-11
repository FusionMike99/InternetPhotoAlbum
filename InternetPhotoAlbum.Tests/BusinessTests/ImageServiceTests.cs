using InternetPhotoAlbum.BLL.Infrastructure;
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
    public class ImageServiceTests
    {
        [Test]
        public void ImageService_FindAll_ReturnsImageDTOs()
        {
            // Arrange
            var expected = GetTestImageDTOs().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.GetAll())
                .Returns(GetTestImageEntities().AsQueryable);
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = imageService.FindAll().ToList();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new ImageDTOEqualityComparer()));
        }

        [Test]
        public async Task ImageService_FindByIdAsync_ReturnsImageDTO()
        {
            // Arrange
            var expected = GetTestImageDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestImageEntities().First);
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;

            // Act
            var actual = await imageService.FindByIdAsync(imageId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ImageDTOEqualityComparer()));
        }

        [Test]
        public void ImageService_FindByIdAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestImageDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Image)null);
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;

            // Act
            Task actual() => imageService.FindByIdAsync(imageId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Test]
        public void ImageService_FindByAlbumId_ReturnsImageDTOs()
        {
            // Arrange
            var expected = GetTestImageDTOs();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.Get(It.IsAny<Func<Image, bool>>()))
                .Returns(GetTestImageEntities());
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumId = 1;

            // Act
            var actual = imageService.FindByAlbumId(albumId);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new ImageDTOEqualityComparer()));
        }

        [Test]
        public async Task ImageService_CreateAsync_AddsModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Create(It.IsAny<Image>()))
                .Returns(GetTestImageEntities().First);
            mockUnitOfWork.Setup(x => x.ProceduresRepository.UpdateAlbumPeriods(It.IsAny<int>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var image = GetTestImageDTOs().First();

            //Act
            await imageService.CreateAsync(image);

            //Assert
            mockUnitOfWork.Verify(x => x.ImagesRepository.Create(It.Is<Image>(i => i.Title == image.Title && i.Id == image.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.ProceduresRepository.UpdateAlbumPeriods(It.Is<int>(i => i == image.AlbumId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void ImageService_CreateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Create(It.IsAny<Image>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            ImageDTO image = null;

            // Act
            Task actual() => imageService.CreateAsync(image);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("", "description")]
        [TestCase("ti", "description")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth", "description")]
        [TestCase("title", "de")]
        public void ImageService_CreateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string title, string description)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Create(It.IsAny<Image>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            ImageDTO image = new ImageDTO
            {
                Id = 5,
                Title = title,
                Description = description,
                AddedDate = new DateTime(2020, 1, 13),
                AlbumId = 1
            };

            // Act
            Task actual() => imageService.CreateAsync(image);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task ImageService_UpdateAsync_UpdatesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Update(It.IsAny<Image>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var image = new ImageDTO
            {
                Id = 5,
                Title = "Album5",
                Description = "Desc5",
                AddedDate = new DateTime(2020, 1, 13),
                AlbumId = 1
            };

            //Act
            await imageService.UpdateAsync(image);

            //Assert
            mockUnitOfWork.Verify(x => x.ImagesRepository.Update(It.Is<Image>(a => a.Title == image.Title && a.Id == image.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void ImageService_UpdateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Update(It.IsAny<Image>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            ImageDTO image = null;

            // Act
            Task actual() => imageService.UpdateAsync(image);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("", "description")]
        [TestCase("ti", "description")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth", "description")]
        [TestCase("title", "de")]
        public void ImageService_UpdateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string title, string description)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ImagesRepository.Update(It.IsAny<Image>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            ImageDTO image = new ImageDTO
            {
                Id = 5,
                Title = title,
                Description = description,
                AddedDate = new DateTime(2020, 1, 13),
                AlbumId = 1
            };

            // Act
            Task actual() => imageService.CreateAsync(image);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task ImageService_DeleteAsync_DeletesModel()
        {
            //Arrange
            var expected = GetTestImageDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestImageEntities().First);
            mockUnitOfWork.Setup(x => x.ImagesRepository.Remove(It.IsAny<Image>()));
            mockUnitOfWork.Setup(x => x.ProceduresRepository.UpdateAlbumPeriods(It.IsAny<int>()));
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;

            //Act
            await imageService.DeleteAsync(imageId);

            //Assert
            mockUnitOfWork.Verify(x => x.ImagesRepository.GetByIdAsync(It.Is<int>(i => i == imageId)), Times.Once);
            mockUnitOfWork.Verify(x => x.ImagesRepository.Remove(It.Is<Image>(a => a.Title == expected.Title && a.Id == expected.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.ProceduresRepository.UpdateAlbumPeriods(It.IsAny<int>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void ImageService_DeleteAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestImageDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ImagesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Image)null);
            IImageService imageService = new ImageService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var imageId = 1;

            // Act
            Task actual() => imageService.DeleteAsync(imageId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        private IEnumerable<ImageDTO> GetTestImageDTOs()
        {
            return new List<ImageDTO>()
            {
                new ImageDTO {Id = 1, Title = "Image1", Description = "Desc1", AddedDate = new DateTime(2010, 6, 24),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new ImageDTO {Id = 2, Title = "Image2", Description = "Desc2", AddedDate = new DateTime(2016, 4, 16),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new ImageDTO {Id = 3, Title = "Image3", Description = "Desc3", AddedDate = new DateTime(2012, 12, 31),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new ImageDTO {Id = 4, Title = "Image4", Description = "Desc4", AddedDate = new DateTime(2008, 5, 9),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } }
            };
        }

        private List<Image> GetTestImageEntities()
        {
            return new List<Image>()
            {
                new Image {Id = 1, Title = "Image1", Description = "Desc1", AddedDate = new DateTime(2010, 6, 24),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new Image {Id = 2, Title = "Image2", Description = "Desc2", AddedDate = new DateTime(2016, 4, 16),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new Image {Id = 3, Title = "Image3", Description = "Desc3", AddedDate = new DateTime(2012, 12, 31),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } },
                new Image {Id = 4, Title = "Image4", Description = "Desc4", AddedDate = new DateTime(2008, 5, 9),
                    AlbumId = 1, File = new byte[4] { 1, 2, 3, 4 } }
            };
        }
    }
}
