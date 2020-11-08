using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCase.Application.Exceptions;
using WebApiTestCase.Application.Mappers;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Application.Services.Common;
using WebApiTestCase.Common.Collections;
using WebApiTestCase.Common.Extensions;
using WebApiTestCase.Data;
using WebApiTestCase.Data.Entities;

namespace WebApiTestCase.Application.Services.User
{
    public class UserService : ApplicationContextService, IUserService
    {
        public UserService(ApplicationContext context) : base(context)
        { }

        public async Task<IPagedList<UserResponseModel>> GetAllAsync(PagedModel paged)
        {
            var pagedList = await Context.Users
                .AsNoTracking()
                .Select(u => u.ToUserResponseModel())
                .ToPagedListAsync(paged.CurrentPage, paged.PageSize);

            return pagedList;
        }

        public async Task<UserResponseModel> GetByIdAsync(int id)
        {
            var user = await GetUserEntity(id);

            return user.ToUserResponseModel();
        }

        public async Task<int> UpdateAsync(int id, UserEditModel model)
        {
            var user = await GetUserEntity(id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await SaveChangesAsync();

            return user.Id;
        }

        private async Task<UserEntity> GetUserEntity(int id)
        {
            var user = await Context.Users.FindAsync(id);

            if (user == null)
                throw new NotFoundException($"Пользователь с идентификатором {id} не найден");
            return user;
        }
    }
}