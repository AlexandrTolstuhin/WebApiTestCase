using NUnit.Framework;
using System.Threading.Tasks;
using WebApiTestCase.Application.Exceptions;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Application.Services.User;
using WebApiTestCase.Data.Entities;

namespace WebApiTestCase.Application.Test.Services
{
    public class UserServiceTest : ApplicationContextServiceTest
    {
        private IUserService _userService;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();

            _userService = new UserService(Context);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_Empty_IPagedList_When_User_Is_Empty()
        {
            var pagedList = await _userService.GetAllAsync(new PagedModel());

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_Empty_IPagedList_When_CurrentPage_Not_Have()
        {
            await SeedContextAsync();

            var currentPage = 1;
            var pageSize = 5;
            var pagedList = await _userService.GetAllAsync(new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsEmpty(pagedList.Items);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_IPagedList()
        {
            await SeedContextAsync();

            var currentPage = 0;
            var pageSize = 5;
            var pagedList = await _userService.GetAllAsync(new PagedModel { CurrentPage = currentPage, PageSize = pageSize });

            Assert.IsNotEmpty(pagedList.Items);
        }

        [Test]
        public void GetByIdAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var userId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetByIdAsync(userId));

            Assert.That(ex.Message, Is.EqualTo($"Пользователь с идентификатором {userId} не найден"));
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_UserResponseModel_If_User_Exist()
        {
            var addedUser = (await Context.Users.AddAsync(new UserEntity
            {
                FirstName = "FirstName",
                LastName = "LastName"
            })).Entity;
            await Context.SaveChangesAsync();

            var user = await _userService.GetByIdAsync(addedUser.Id);

            Assert.AreEqual(addedUser.Id, user.Id);
            Assert.AreEqual(addedUser.FirstName, user.FirstName);
            Assert.AreEqual(addedUser.LastName, user.LastName);
            Assert.AreEqual(addedUser.Status, user.Status);
        }

        [Test]
        public void UpdateAsync_Should_Throw_NotFoundException_When_User_Not_Have_In_Context()
        {
            var userId = 42;

            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.UpdateAsync(userId, new UserEditModel()));

            Assert.That(ex.Message, Is.EqualTo($"Пользователь с идентификатором {userId} не найден"));
        }

        [Test]
        public async Task UpdateAsync_Should_Return_User_Id_If_Success()
        {
            var addedUser = (await Context.Users.AddAsync(new UserEntity
            {
                FirstName = "FirstName",
                LastName = "LastName"
            })).Entity;
            await Context.SaveChangesAsync();

            var editModel = new UserEditModel
            {
                FirstName = "New FirstName",
                LastName = "New LastName"
            };

            var id = await _userService.UpdateAsync(addedUser.Id, editModel);

            Assert.AreEqual(addedUser.Id, id);
            Assert.AreEqual(editModel.FirstName, addedUser.FirstName);
            Assert.AreEqual(editModel.LastName, addedUser.LastName);
            Assert.IsTrue(addedUser.CreatedDate < addedUser.ModifiedDate);
        }
    }
}