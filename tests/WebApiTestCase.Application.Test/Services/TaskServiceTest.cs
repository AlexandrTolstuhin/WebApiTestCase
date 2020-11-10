using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCase.Application.Exceptions;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Services.Task;
using TaskStatus = WebApiTestCase.Core.Enums.TaskStatus;

namespace WebApiTestCase.Application.Test.Services
{
    public class TaskServiceTest : ApplicationContextServiceTest
    {
        private ITaskService _taskService;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();

            _taskService = new TaskService(Context);
        }

        #region CreateAsync Tests
        [Test]
        public async Task CreateAsync_Should_Return_Created_Task_Id()
        {
            await SeedContextAsync();

            var users = Context.Users.Take(2).ToArray();

            var createModel = new TaskCreateModel
            {
                Name = "Valid task name",
                ProviderId = users[0].Id,
                PerformerId = users[1].Id
            };

            var id = await _taskService.CreateAsync(createModel);

            var createdTask = Context.Tasks.Find(id);

            Assert.Positive(id);
            Assert.AreEqual(createModel.Name, createdTask.Name);
            Assert.AreEqual(createModel.Description, createdTask.Description);
            Assert.AreEqual(createModel.ProviderId, createdTask.ProviderId);
            Assert.AreEqual(createModel.PerformerId, createdTask.PerformerId);
        }
        #endregion

        #region GetUserProvidedTasksAsync Tests
        [Test]
        public async Task GetUserProvidedTasksAsync_Should_Return_Empty_IPagedList_When_Tasks_Is_Empty()
        {
            var pagedList = await _taskService.GetUserProvidedTasksAsync(42, new PagedModel());

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetUserProvidedTasksAsync_Should_Return_Empty_IPagedList_When_CurrentPage_Not_Have()
        {
            await SeedContextAsync();

            var currentPage = 1;
            var pageSize = 50;
            var pagedList = await _taskService.GetUserProvidedTasksAsync(1, new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetUserProvidedTasksAsync_Should_Return_IPagedList()
        {
            await SeedContextAsync();

            var currentPage = 0;
            var pageSize = 5;
            var pagedList = await _taskService.GetUserProvidedTasksAsync(1, new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsNotEmpty(pagedList.Items);
        }
        #endregion

        #region GetUserPerformedTasksAsync Tests
        [Test]
        public async Task GetUserPerformedTasksAsync_Should_Return_Empty_IPagedList_When_Tasks_Is_Empty()
        {
            var pagedList = await _taskService.GetUserPerformedTasksAsync(42, new PagedModel());

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetUserPerformedTasksAsync_Should_Return_Empty_IPagedList_When_CurrentPage_Not_Have()
        {
            await SeedContextAsync();

            var currentPage = 1;
            var pageSize = 50;
            var pagedList = await _taskService.GetUserPerformedTasksAsync(1, new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetUserPerformedTasksAsync_Should_Return_IPagedList()
        {
            await SeedContextAsync();

            var currentPage = 0;
            var pageSize = 5;
            var pagedList = await _taskService.GetUserPerformedTasksAsync(1, new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsNotEmpty(pagedList.Items);
        }
        #endregion

        #region GetByIdAsync Tests
        [Test]
        public void GetByIdAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var taskId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _taskService.GetTaskByIdAsync(taskId));

            Assert.That(ex.Message, Is.EqualTo($"Задача с идентификатором {taskId} не найдена"));
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_UserResponseModel_If_User_Exist()
        {
            await SeedContextAsync();

            var existTask = Context.Tasks.First();

            var task = await _taskService.GetTaskByIdAsync(existTask.Id);

            Assert.AreEqual(existTask.Id, task.Id);
            Assert.AreEqual(existTask.Name, task.Name);
            Assert.AreEqual(existTask.ProviderId, task.ProviderId);
            Assert.AreEqual(existTask.PerformerId, task.PerformerId);
            Assert.AreEqual(existTask.Status, task.Status);
        }
        #endregion

        #region UpdateAsync Tests
        [Test]
        public void UpdateAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var taskId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _taskService.UpdateAsync(taskId, new TaskEditModel()));

            Assert.That(ex.Message, Is.EqualTo($"Задача с идентификатором {taskId} не найдена"));
        }

        [Test]
        public async Task UpdateAsync_Should_Return_User_Id_If_Success()
        {
            await SeedContextAsync();

            var existTask = Context.Tasks.First();

            var editModel = new TaskEditModel
            {
                Name = "New task name",
                Description = "New description",
                PerformerId = 3
            };

            var id = await _taskService.UpdateAsync(existTask.Id, editModel);

            Assert.AreEqual(existTask.Id, id);
            Assert.AreEqual(existTask.Name, editModel.Name);
            Assert.AreEqual(existTask.Description, editModel.Description);
            Assert.AreEqual(existTask.PerformerId, editModel.PerformerId);
            Assert.IsTrue(existTask.CreatedDate < existTask.ModifiedDate);
        }
        #endregion

        #region SetProviderAsync Tests
        [Test]
        public void SetProviderAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var taskId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _taskService.SetProviderAsync(taskId, new TaskProviderEditModel()));

            Assert.That(ex.Message, Is.EqualTo($"Задача с идентификатором {taskId} не найдена"));
        }

        [Test]
        public async Task SetProviderAsync_Should_Return_User_Id_If_Success()
        {
            await SeedContextAsync();

            var existTask = Context.Tasks.First();

            var editModel = new TaskProviderEditModel
            {
                ProviderId = 3
            };

            var id = await _taskService.SetProviderAsync(existTask.Id, editModel);

            Assert.AreEqual(existTask.Id, id);
            Assert.AreEqual(existTask.ProviderId, editModel.ProviderId);
            Assert.IsTrue(existTask.CreatedDate < existTask.ModifiedDate);
        }
        #endregion

        #region SetStatusAsync Tests
        [Test]
        public void SetStatusAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var taskId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _taskService.SetStatusAsync(taskId, new TaskStatusEditModel()));

            Assert.That(ex.Message, Is.EqualTo($"Задача с идентификатором {taskId} не найдена"));
        }

        [Test]
        public async Task SetStatusAsync_Should_Return_User_Id_If_Success()
        {
            await SeedContextAsync();

            var existTask = Context.Tasks.First();

            var editModel = new TaskStatusEditModel
            {
                Status = TaskStatus.Completed
            };

            var id = await _taskService.SetStatusAsync(existTask.Id, editModel);

            Assert.AreEqual(existTask.Id, id);
            Assert.AreEqual(existTask.Status, editModel.Status);
            Assert.IsTrue(existTask.CreatedDate < existTask.ModifiedDate);
        }
        #endregion
    }
}