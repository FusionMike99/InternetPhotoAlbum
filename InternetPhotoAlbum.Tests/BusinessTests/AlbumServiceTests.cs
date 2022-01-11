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
    public class AlbumServiceTests
    {
        [Test]
        public void AlbumService_FindAll_ReturnsAlbumDTOs()
        {
            // Arrange
            var expected = GetTestAlbumDTOs().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.GetAll())
                .Returns(GetTestAlbumEntities().AsQueryable);
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = albumService.FindAll().ToList();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new AlbumDTOEqualityComparer()));
        }

        [Test]
        public async Task AlbumService_FindByIdAsync_ReturnsAlbumDTO()
        {
            // Arrange
            var expected = GetTestAlbumDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestAlbumEntities().First);
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumId = 1;

            // Act
            var actual = await albumService.FindByIdAsync(albumId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new AlbumDTOEqualityComparer()));
        }

        [Test]
        public void AlbumService_FindByIdAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestAlbumDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Album)null);
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumId = 1;

            // Act
            Task actual() => albumService.FindByIdAsync(albumId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Test]
        public void AlbumService_FindByUserId_ReturnsAlbumDTOs()
        {
            // Arrange
            var expected = GetTestAlbumDTOs();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.Get(It.IsAny<Func<Album, bool>>()))
                .Returns(GetTestAlbumEntities());
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var userId = "1";

            // Act
            var actual = albumService.FindByUserId(userId);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new AlbumDTOEqualityComparer()));
        }

        [Test]
        public async Task AlbumService_CreateAsync_AddsModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Create(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = new AlbumDTO
            {
                Id = 5,
                Title = "Album5",
                Description = "Desc5",
                PeriodStart = new DateTime(2020, 1, 13),
                PeriodEnd = new DateTime(2020, 6, 14),
                UserId = "1"
            };

            //Act
            await albumService.CreateAsync(album);

            //Assert
            mockUnitOfWork.Verify(x => x.AlbumsRepository.Create(It.Is<Album>(a => a.Title == album.Title && a.Id == album.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void AlbumService_CreateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Create(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            AlbumDTO album = null;

            // Act
            Task actual() => albumService.CreateAsync(album);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("", "description", "1")]
        [TestCase("ti", "description", "1")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth", "description", "1")]
        [TestCase("title", "de", "1")]
        [TestCase("title", "description", "")]
        public void AlbumService_CreateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string title, string description, string userId)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Create(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            AlbumDTO album = new AlbumDTO
            {
                Id = 5,
                Title = title,
                Description = description,
                PeriodStart = new DateTime(2020, 1, 13),
                PeriodEnd = new DateTime(2020, 6, 14),
                UserId = userId
            };

            // Act
            Task actual() => albumService.CreateAsync(album);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task AlbumService_UpdateAsync_UpdatesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Update(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var album = new AlbumDTO
            {
                Id = 5,
                Title = "Album5",
                Description = "Desc5",
                PeriodStart = new DateTime(2020, 1, 13),
                PeriodEnd = new DateTime(2020, 6, 14),
                UserId = "1"
            };

            //Act
            await albumService.UpdateAsync(album);

            //Assert
            mockUnitOfWork.Verify(x => x.AlbumsRepository.Update(It.Is<Album>(a => a.Title == album.Title && a.Id == album.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void AlbumService_UpdateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Update(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            AlbumDTO album = null;

            // Act
            Task actual() => albumService.UpdateAsync(album);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("", "description", "1")]
        [TestCase("ti", "description", "1")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth", "description", "1")]
        [TestCase("title", "de", "1")]
        [TestCase("title", "description", "")]
        public void AlbumService_UpdateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string title, string description, string userId)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Update(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            AlbumDTO album = new AlbumDTO
            {
                Id = 5,
                Title = title,
                Description = description,
                PeriodStart = new DateTime(2020, 1, 13),
                PeriodEnd = new DateTime(2020, 6, 14),
                UserId = userId
            };

            // Act
            Task actual() => albumService.CreateAsync(album);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task AlbumService_DeleteAsync_DeletesModel()
        {
            //Arrange
            var expected = GetTestAlbumDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestAlbumEntities().First);
            mockUnitOfWork.Setup(x => x.AlbumsRepository.Remove(It.IsAny<Album>()));
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumId = 1;

            //Act
            await albumService.DeleteAsync(albumId);

            //Assert
            mockUnitOfWork.Verify(x => x.AlbumsRepository.GetByIdAsync(It.Is<int>(i => i == albumId)), Times.Once);
            mockUnitOfWork.Verify(x => x.AlbumsRepository.Remove(It.Is<Album>(a => a.Title == expected.Title && a.Id == expected.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void AlbumService_DeleteAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestAlbumDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.AlbumsRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Album)null);
            IAlbumService albumService = new AlbumService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var albumId = 1;

            // Act
            Task actual() => albumService.DeleteAsync(albumId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        private IEnumerable<AlbumDTO> GetTestAlbumDTOs()
        {
            return new List<AlbumDTO>()
            {
                new AlbumDTO {Id = 1, Title = "Album1", Description = "Desc1", PeriodStart = new DateTime(2010, 6, 24),
                    PeriodEnd = new DateTime(2014, 7, 13), UserId = "1" },
                new AlbumDTO {Id = 2, Title = "Album2", Description = "Desc2", PeriodStart = new DateTime(2016, 4, 16),
                    PeriodEnd = new DateTime(2017, 2, 28), UserId = "1" },
                new AlbumDTO {Id = 3, Title = "Album3", Description = "Desc3", PeriodStart = new DateTime(2012, 12, 31),
                    PeriodEnd = new DateTime(2015, 8, 9), UserId = "1" },
                new AlbumDTO {Id = 4, Title = "Album4", Description = "Desc4", PeriodStart = new DateTime(2008, 5, 9),
                    PeriodEnd = new DateTime(2009, 11, 16), UserId = "1" }
            };
        }

        private List<Album> GetTestAlbumEntities()
        {
            return new List<Album>()
            {
                new Album {Id = 1, Title = "Album1", Description = "Desc1", PeriodStart = new DateTime(2010, 6, 24),
                    PeriodEnd = new DateTime(2014, 7, 13), UserId = "1" },
                new Album {Id = 2, Title = "Album2", Description = "Desc2", PeriodStart = new DateTime(2016, 4, 16),
                    PeriodEnd = new DateTime(2017, 2, 28), UserId = "1" },
                new Album {Id = 3, Title = "Album3", Description = "Desc3", PeriodStart = new DateTime(2012, 12, 31),
                    PeriodEnd = new DateTime(2015, 8, 9), UserId = "1" },
                new Album {Id = 4, Title = "Album4", Description = "Desc4", PeriodStart = new DateTime(2008, 5, 9),
                    PeriodEnd = new DateTime(2009, 11, 16), UserId = "1" }
            };
        }
    }
}
