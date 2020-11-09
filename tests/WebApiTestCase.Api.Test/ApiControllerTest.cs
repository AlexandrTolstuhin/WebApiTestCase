using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Services.Task;
using WebApiTestCase.Application.Services.User;

namespace WebApiTestCase.Api.Test
{
    public abstract class ApiControllerTest
    {
        protected Mock<IUserService> UserServiceMock;
        protected Mock<ITaskService> TaskServiceMock;

        public virtual void Setup()
        {
            UserServiceMock = new Mock<IUserService>();
            TaskServiceMock = new Mock<ITaskService>();
        }

        protected void AssertOkObjectResult(IStatusCodeActionResult result)
        {
            Assert.NotNull(result);
            Assert.NotNull(result.StatusCode);
            Assert.AreEqual(200, result.StatusCode.Value);
        }

        protected void AssertApiResult<T>(ApiResult<T> apiResult)
        {
            Assert.NotNull(apiResult);
            Assert.AreEqual(200, apiResult.Code);
            Assert.NotNull(apiResult.Result);
            Assert.IsEmpty(apiResult.Errors);
            Assert.IsTrue(apiResult.Succeeded);
        }
    }
}