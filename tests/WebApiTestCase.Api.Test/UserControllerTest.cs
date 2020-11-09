using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebApiTestCase.Api.Controllers;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Api.Test
{
    public class UserControllerTest : ApiControllerTest
    {
        private UserController _userController;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _userController = new UserController(UserServiceMock.Object, TaskServiceMock.Object);
        }

        [Test]
        public async Task Get_By_Id_Should_Return_ApiResult_With_UserResponseModel()
        {
            var userId = 42;

            var response = new UserResponseModel
            {
                Id = userId
            };

            UserServiceMock
                .Setup(s => s.GetByIdAsync(userId))
                .ReturnsAsync(response);

            var result = await _userController.Get(userId) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<UserResponseModel>;

            AssertApiResult(apiResult);
            Assert.AreEqual(userId, apiResult?.Result.Id);
        }

        [Test]
        public async Task Get_Should_Return_ApiResult_With_IPagedList_UserResponseModel()
        {
            var paged = new PagedModel();

            UserServiceMock
                .Setup(s => s.GetAllAsync(paged))
                .ReturnsAsync(PagedList.Empty<UserResponseModel>());

            var result = await _userController.Get(paged) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<IPagedList<UserResponseModel>>;

            AssertApiResult(apiResult);
        }

        [Test]
        public async Task Put_Should_Return_ApiResult_With_Edit_User_Id()
        {
            var userId = 42;

            var editModel = new UserEditModel();

            UserServiceMock
                .Setup(s => s.UpdateAsync(userId, editModel))
                .ReturnsAsync(userId);

            var result = await _userController.Put(userId, editModel) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<int>;

            AssertApiResult(apiResult);
            Assert.AreEqual(userId, apiResult?.Result);
        }

        [Test]
        public async Task Post_Should_Return_ApiResult_With_Created_Task_Id()
        {
            var taskId = 42;

            var createModel = new TaskCreateModel();

            TaskServiceMock
                .Setup(s => s.CreateAsync(createModel))
                .ReturnsAsync(taskId);

            var result = await _userController.Post(createModel) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<int>;

            AssertApiResult(apiResult);
            Assert.AreEqual(taskId, apiResult?.Result);
        }
    }
}