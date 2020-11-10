using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebApiTestCase.Api.Controllers;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Api.Test
{
    public class TaskControllerTest : ApiControllerTest
    {
        private TaskController _taskController;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _taskController = new TaskController(TaskServiceMock.Object);
        }

        [Test]
        public async Task Get_By_Id_Should_Return_ApiResult_With_TaskResponseModel()
        {
            var taskId = 42;

            var responseModel = new TaskResponseModel { Id = taskId };

            TaskServiceMock
                .Setup(s => s.GetTaskByIdAsync(taskId))
                .ReturnsAsync(responseModel);

            var result = await _taskController.Get(taskId) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<TaskResponseModel>;

            AssertApiResult(apiResult);
            Assert.AreEqual(taskId, apiResult?.Result.Id);
        }

        [Test]
        public async Task GetProvided_Should_Return_ApiResult_With_IPagedList_TaskResponseModel()
        {
            var userId = 42;
            var paged = new PagedModel();

            TaskServiceMock
                .Setup(s => s.GetUserProvidedTasksAsync(userId, paged))
                .ReturnsAsync(PagedList.Empty<TaskResponseModel>());

            var result = await _taskController.GetProvided(userId, paged) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<IPagedList<TaskResponseModel>>;

            AssertApiResult(apiResult);
        }

        [Test]
        public async Task GetPerformed_Should_Return_ApiResult_With_IPagedList_TaskResponseModel()
        {
            var userId = 42;
            var paged = new PagedModel();

            TaskServiceMock
                .Setup(s => s.GetUserProvidedTasksAsync(userId, paged))
                .ReturnsAsync(PagedList.Empty<TaskResponseModel>());

            var result = await _taskController.GetProvided(userId, paged) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<IPagedList<TaskResponseModel>>;

            AssertApiResult(apiResult);
        }

        [Test]
        public async Task Put_Should_Return_ApiResult_With_Edit_Task_Id()
        {
            var taskId = 42;
            var editModel = new TaskEditModel();

            TaskServiceMock
                .Setup(m => m.UpdateAsync(taskId, editModel))
                .ReturnsAsync(taskId);

            var result = await _taskController.Put(taskId, editModel) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<int>;

            AssertApiResult(apiResult);
            Assert.AreEqual(taskId, apiResult?.Result);
        }

        [Test]
        public async Task PutStatus_Should_Return_ApiResult_With_Edit_Task_Id()
        {
            var taskId = 42;
            var editModel = new TaskStatusEditModel();

            TaskServiceMock
                .Setup(m => m.SetStatusAsync(taskId, editModel))
                .ReturnsAsync(taskId);

            var result = await _taskController.PutStatus(taskId, editModel) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<int>;

            AssertApiResult(apiResult);
            Assert.AreEqual(taskId, apiResult?.Result);
        }

        [Test]
        public async Task PutProvider_Should_Return_ApiResult_With_Edit_Task_Id()
        {
            var taskId = 42;
            var editModel = new TaskProviderEditModel();

            TaskServiceMock
                .Setup(m => m.SetProviderAsync(taskId, editModel))
                .ReturnsAsync(taskId);

            var result = await _taskController.PutProvider(taskId, editModel) as OkObjectResult;

            AssertOkObjectResult(result);

            var apiResult = result?.Value as ApiResult<int>;

            AssertApiResult(apiResult);
            Assert.AreEqual(taskId, apiResult?.Result);
        }
    }
}