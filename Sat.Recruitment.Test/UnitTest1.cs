using System;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Dto;
using Sat.Recuitment.Service.Interfaces;
using Sat.Recuitment.Service.Services;
using Xunit;
using Moq;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infraestructure.Interfaces;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly IMapper _mapper;
        private readonly UsersController _userController;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private UserDto _userDto;

        public UnitTest1()
        {
            //////////////////////////////////////////////////
            /// Repository and some service tests pending
            ////////////////////////////////////////////////////
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, UserModel>());
            _mapper = configuration.CreateMapper();
            //_mockMapper = new Mock<IMapper>();
            _mockUserService = new Mock<IUserService>();
            _mockUserRepository = new Mock<IUserRepository>();
            

            _userController = new UsersController(_mockUserService.Object, _mapper);

            _userDto = new UserDto
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Money = "124",
                Phone = "+349 1122354215",
                UserType = "Normal"
            };
        }

        //[Fact]
        //public void Test1()
        //{
        //    var userController = new UsersController(null,null);

        //    var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


        //    Assert.Equal(true, result.IsSuccess);
        //    Assert.Equal("User Created", result.Message);
        //}

        //[Fact]
        //public void Test2()
        //{
        //    var userController = new UsersController();

        //    var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;


        //    Assert.Equal(false, result.IsSuccess);
        //    Assert.Equal("The user is duplicated", result.Errors);
        //}

        [Fact]
        public async Task ControllerSuccessPath()
        {
            var userDto = new UserDto
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Money = "124",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };

            _mockUserService.Setup(us => us.SaveUserAsync(It.IsAny<UserModel>())).Returns(Task.FromResult(true));
            var result = await _userController.CreateUser(userDto) as Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public async Task ControllerErrorPath()
        {
            var userDto = new UserDto
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Money = "124",
                Phone = "+349 1122354215",
                UserType = "Normal"

            };

            _mockUserService.Setup(us => us.SaveUserAsync(It.IsAny<UserModel>())).Returns(Task.FromResult(false));
            var result = await _userController.CreateUser(userDto) as Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("User not created", result.Message);
        }

        [Fact]
        public void UserMapperTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserDto, UserModel>());

           configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void UserMapperPropertiesTest()
        {
            var result =_mapper.Map<UserModel>(_userDto);

            Assert.Equal(result.Address, _userDto.Address);
            Assert.Equal(result.Email, _userDto.Email);
            Assert.Equal(result.Name, _userDto.Name);
            Assert.Equal(result.Phone, _userDto.Phone);
            Assert.Equal(result.UserType, _userDto.UserType);
        }

        [Fact]
        public async Task UserServiceTest()
        {
            var userService = new UserService(_mockUserRepository.Object);
            var userModel = _mapper.Map<UserModel>(_userDto);
            _mockUserRepository.Setup(ur => ur.CreateUserAsync(It.IsAny<UserModel>())).Returns(Task.FromResult(true));

            var result = await userService.SaveUserAsync(userModel);

            Assert.Equal("mike@gmail.com", userModel.Email);
            Assert.Equal(138.88m, userModel.Money);
            Assert.True(result);
        }

    }
}
