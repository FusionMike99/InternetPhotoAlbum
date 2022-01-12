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
    public class LikeTypeServiceTests
    {
        [Test]
        public void LikeTypeService_FindAll_ReturnsLikeTypeDTOs()
        {
            // Arrange
            var expected = GetTestLikeTypeDTOs().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LikeTypesRepository.GetAll())
                .Returns(GetTestLikeTypeEntities().AsQueryable);
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = service.FindAll().ToList();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new LikeTypeDTOEqualityComparer()));
        }

        [Test]
        public async Task LikeTypeService_FindByIdAsync_ReturnsLikeTypeDTO()
        {
            // Arrange
            var expected = GetTestLikeTypeDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LikeTypesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestLikeTypeEntities().First);
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeTypeId = 1;

            // Act
            var actual = await service.FindByIdAsync(likeTypeId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new LikeTypeDTOEqualityComparer()));
        }

        [Test]
        public void LikeTypeService_FindByIdAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestLikeTypeDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LikeTypesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((LikeType)null);
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeTypeId = 1;

            // Act
            Task actual() => service.FindByIdAsync(likeTypeId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Test]
        public async Task LikeTypeService_CreateAsync_AddsModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Create(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeType = new LikeTypeDTO
            {
                Id = 5,
                Name = "LikeType5"
            };

            //Act
            await service.CreateAsync(likeType);

            //Assert
            mockUnitOfWork.Verify(x => x.LikeTypesRepository.Create(It.Is<LikeType>(g => g.Name == likeType.Name && g.Id == likeType.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void LikeTypeService_CreateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Create(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            LikeTypeDTO likeType = null;

            // Act
            Task actual() => service.CreateAsync(likeType);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("")]
        [TestCase("ti")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth")]
        public void LikeTypeService_CreateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string name)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Create(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            LikeTypeDTO likeType = new LikeTypeDTO
            {
                Id = 5,
                Name = name
            };

            // Act
            Task actual() => service.CreateAsync(likeType);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task LikeTypeService_UpdateAsync_UpdatesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Update(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeType = new LikeTypeDTO
            {
                Id = 5,
                Name = "LikeType5"
            };

            //Act
            await service.UpdateAsync(likeType);

            //Assert
            mockUnitOfWork.Verify(x => x.LikeTypesRepository.Update(It.Is<LikeType>(g => g.Name == likeType.Name && g.Id == likeType.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void LikeTypeService_UpdateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Update(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            LikeTypeDTO likeType = null;

            // Act
            Task actual() => service.UpdateAsync(likeType);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("")]
        [TestCase("ti")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth")]
        public void LikeTypeService_UpdateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string name)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Update(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            LikeTypeDTO likeType = new LikeTypeDTO
            {
                Id = 5,
                Name = name
            };

            // Act
            Task actual() => service.UpdateAsync(likeType);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task LikeTypeService_DeleteAsync_DeletesModel()
        {
            //Arrange
            var expected = GetTestLikeTypeDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LikeTypesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestLikeTypeEntities().First);
            mockUnitOfWork.Setup(x => x.LikeTypesRepository.Remove(It.IsAny<LikeType>()));
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeTypeId = 1;

            //Act
            await service.DeleteAsync(likeTypeId);

            //Assert
            mockUnitOfWork.Verify(x => x.LikeTypesRepository.GetByIdAsync(It.Is<int>(i => i == likeTypeId)), Times.Once);
            mockUnitOfWork.Verify(x => x.LikeTypesRepository.Remove(It.Is<LikeType>(g => g.Name == expected.Name && g.Id == expected.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void LikeTypeService_DeleteAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestLikeTypeDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.LikeTypesRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((LikeType)null);
            ILikeTypeService service = new LikeTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var likeTypeId = 1;

            // Act
            Task actual() => service.DeleteAsync(likeTypeId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        private IEnumerable<LikeTypeDTO> GetTestLikeTypeDTOs()
        {
            return new List<LikeTypeDTO>()
            {
                new LikeTypeDTO {Id = 1, Name = "LikeType1" },
                new LikeTypeDTO {Id = 2, Name = "LikeType2" },
                new LikeTypeDTO {Id = 3, Name = "LikeType3" },
                new LikeTypeDTO {Id = 4, Name = "LikeType4" }
            };
        }

        private List<LikeType> GetTestLikeTypeEntities()
        {
            return new List<LikeType>()
            {
                new LikeType {Id = 1, Name = "LikeType1" },
                new LikeType {Id = 2, Name = "LikeType2" },
                new LikeType {Id = 3, Name = "LikeType3" },
                new LikeType {Id = 4, Name = "LikeType4" }
            };
        }
    }
}
