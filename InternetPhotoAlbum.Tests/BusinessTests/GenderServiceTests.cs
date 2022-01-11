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
    public class GenderServiceTests
    {
        [Test]
        public void GenderService_FindAll_ReturnsGenderDTOs()
        {
            // Arrange
            var expected = GetTestGenderDTOs().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.GendersRepository.GetAll())
                .Returns(GetTestGenderEntities().AsQueryable);
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = service.FindAll().ToList();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new GenderDTOEqualityComparer()));
        }

        [Test]
        public async Task GenderService_FindByIdAsync_ReturnsGenderDTO()
        {
            // Arrange
            var expected = GetTestGenderDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.GendersRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestGenderEntities().First);
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var genderId = 1;

            // Act
            var actual = await service.FindByIdAsync(genderId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new GenderDTOEqualityComparer()));
        }

        [Test]
        public void GenderService_FindByIdAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestGenderDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.GendersRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Gender)null);
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var genderId = 1;

            // Act
            Task actual() => service.FindByIdAsync(genderId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Test]
        public async Task GenderService_CreateAsync_AddsModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Create(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var gender = new GenderDTO
            {
                Id = 5,
                Name = "Gender5"
            };

            //Act
            await service.CreateAsync(gender);

            //Assert
            mockUnitOfWork.Verify(x => x.GendersRepository.Create(It.Is<Gender>(g => g.Name == gender.Name && g.Id == gender.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void GenderService_CreateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Create(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            GenderDTO gender = null;

            // Act
            Task actual() => service.CreateAsync(gender);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("")]
        [TestCase("ti")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth")]
        public void GenderService_CreateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string name)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Create(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            GenderDTO gender = new GenderDTO
            {
                Id = 5,
                Name = name
            };

            // Act
            Task actual() => service.CreateAsync(gender);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task GenderService_UpdateAsync_UpdatesModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Update(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var gender = new GenderDTO
            {
                Id = 5,
                Name = "Gender5"
            };

            //Act
            await service.UpdateAsync(gender);

            //Assert
            mockUnitOfWork.Verify(x => x.GendersRepository.Update(It.Is<Gender>(g => g.Name == gender.Name && g.Id == gender.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void GenderService_UpdateAsync_ThrowsArgumentNullExceptionWithNullModel()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Update(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            GenderDTO gender = null;

            // Act
            Task actual() => service.UpdateAsync(gender);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(actual);
        }

        [TestCase("")]
        [TestCase("ti")]
        [TestCase("tibfbnfgdndndfnhhtdhttrhth")]
        public void GenderService_UpdateAsync_ThrowsAggregateValidationExceptionWithInvalidModel(string name)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GendersRepository.Update(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            GenderDTO gender = new GenderDTO
            {
                Id = 5,
                Name = name
            };

            // Act
            Task actual() => service.UpdateAsync(gender);

            // Assert
            Assert.ThrowsAsync<AggregateValidationException>(actual);
        }

        [Test]
        public async Task GenderService_DeleteAsync_DeletesModel()
        {
            //Arrange
            var expected = GetTestGenderDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.GendersRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestGenderEntities().First);
            mockUnitOfWork.Setup(x => x.GendersRepository.Remove(It.IsAny<Gender>()));
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var genderId = 1;

            //Act
            await service.DeleteAsync(genderId);

            //Assert
            mockUnitOfWork.Verify(x => x.GendersRepository.GetByIdAsync(It.Is<int>(i => i == genderId)), Times.Once);
            mockUnitOfWork.Verify(x => x.GendersRepository.Remove(It.Is<Gender>(g => g.Name == expected.Name && g.Id == expected.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void GenderService_DeleteAsync_ThrowsInvalidOperationExceptionWithNullEntity()
        {
            // Arrange
            var expected = GetTestGenderDTOs().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.GendersRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Gender)null);
            IGenderService service = new GenderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var genderId = 1;

            // Act
            Task actual() => service.DeleteAsync(genderId);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        private IEnumerable<GenderDTO> GetTestGenderDTOs()
        {
            return new List<GenderDTO>()
            {
                new GenderDTO {Id = 1, Name = "Gender1" },
                new GenderDTO {Id = 2, Name = "Gender2" },
                new GenderDTO {Id = 3, Name = "Gender3" },
                new GenderDTO {Id = 4, Name = "Gender4" }
            };
        }

        private List<Gender> GetTestGenderEntities()
        {
            return new List<Gender>()
            {
                new Gender {Id = 1, Name = "Gender1" },
                new Gender {Id = 2, Name = "Gender2" },
                new Gender {Id = 3, Name = "Gender3" },
                new Gender {Id = 4, Name = "Gender4" }
            };
        }
    }
}
