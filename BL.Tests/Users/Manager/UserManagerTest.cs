using System;
using AutoMapper;
using BL.Users.Entities;
using BL.Users.Manager;
using DataAccess.Entities;
using DataAccess.Repository;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BL.Tests.Users.Manager
{
    [TestFixture]
    [TestOf(typeof(UserManager))]
    [Category("Unit")]
    public class UserManagerTest
    {
        private IMapper _mapper;
        private Mock<IRepository<UserEntity>> _repositoryMock;
        private UserManager _manager;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserModel, UserEntity>();
                cfg.CreateMap<UpdateUserModel, UserEntity>();
                cfg.CreateMap<UserEntity, UserModel>();
            });
            _mapper = mapperConfig.CreateMapper();

            _repositoryMock = new Mock<IRepository<UserEntity>>();
            _manager = new UserManager(_repositoryMock.Object, _mapper);
        }

        [SetUp]
        public void Setup()
        {
            _repositoryMock.Reset();
        }

        [Test]
        public void CreateUser_ShouldAddUserToRepository()
        {
            var createModel = new CreateUserModel
            {
                Login = "test_user",
                Email = "test@example.com",
                FullName = "Test User",
                StateId = 1
            };

            var userEntity = new UserEntity
            {
                Id = 0,
                UserName = createModel.Login,
                FullName = createModel.FullName,
                Email = createModel.Email,
                StateId = createModel.StateId
            };
            
            _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(user => user.Id = 1)  
                .Returns((UserEntity user) => user); 
            
            var result = _manager.CreateUser(createModel);
            
            _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
            result.Id.Should().Be(1);
            result.FullName.Should().Be("Test User");
            result.Email.Should().Be("test@example.com");
            result.StateId.Should().Be(1);
        }

        [Test]
        public void DeleteUser_ShouldRemoveUserFromRepository()
        {
            var user = new UserEntity
            {
                Id = 1,
                UserName = "delete_user",
                FullName = "Delete User",
                Email = "delete@example.com",
                StateId = 1,
                CreationTime = DateTime.UtcNow
            };

            _repositoryMock.Setup(repo => repo.GetById(1)).Returns(user);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<UserEntity>()));
            
            _manager.DeleteUser(1);
            
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<UserEntity>()), Times.Once);
        }

        [Test]
        public void UpdateUser_ShouldModifyExistingUser()
        {
            var user = new UserEntity
            {
                Id = 2,
                FullName = "Old Name",
                Email = "update@example.com",
                StateId = 1,
                CreationTime = DateTime.UtcNow
            };

            var updateModel = new UpdateUserModel
            {
                FullName = "Updated Name",
                Email = "updated@example.com",
                StateId = 2
            };

            _repositoryMock.Setup(repo => repo.GetById(2)).Returns(user);
            _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(u => 
                {
                    u.FullName = updateModel.FullName;
                    u.Email = updateModel.Email;
                    u.StateId = updateModel.StateId;
                })
                .Returns((UserEntity u) => u);

            var updateUserModel = _manager.UpdateUser(updateModel, 2);
            
            _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
            updateUserModel.FullName.Should().Be("Updated Name");
            updateUserModel.Email.Should().Be("updated@example.com");
            updateUserModel.StateId.Should().Be(2);
        }
    }
}
