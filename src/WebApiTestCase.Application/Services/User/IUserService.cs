using System.Threading.Tasks;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Application.Services.User
{
    public interface IUserService
    {
        Task<IPagedList<UserResponseModel>> GetAllAsync(PagedModel paged);

        Task<UserResponseModel> GetByIdAsync(int id);

        Task<int> UpdateAsync(int id, UserEditModel model);
    }
}