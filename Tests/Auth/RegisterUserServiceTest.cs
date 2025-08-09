using Application.Interfaces;
using Application.Services;
using Domain.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Auth
{
    public class RegisterUserServiceTest
    {
        [Fact]
        public async Task RegisterUser_ShouldThrow_WhenEmailExists()
        {
            //Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var service = new RegisterUserService(mockRepo.Object);
            //Act==>RegisterAsync
            //Assert==>ThrowsAsync
            await Assert.ThrowsAsync<Exception>(() =>
                service.RegisterAsync("test@example.com", "username", "password", UserRole.Admin));
        }

        [Fact]
        public async Task RegisterUser_ShouldAddUser_WhenEmailDoesNotExist()
        {
            //Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var service = new RegisterUserService(mockRepo.Object);
            //act
            await service.RegisterAsync("newuser@example.com", "username", "password", UserRole.Admin);
            //Assert
            mockRepo.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == "newuser@example.com")), Times.Once);
        }
    }
}
