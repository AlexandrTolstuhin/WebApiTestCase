using System.Threading.Tasks;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Common.Collections;

namespace WebApiTestCase.Application.Services
{
    public interface ITaskService
    {
        Task<IPagedList<TaskResponseModel>> GetUserProvidedTasksAsync(int userId, PagedModel paged);

        Task<IPagedList<TaskResponseModel>> GetUserPerformedTasksAsync(int userId, PagedModel paged);

        Task<TaskResponseModel> GetTaskByIdAsync(int id);

        Task<int> UpdateAsync(int id, TaskEditModel model);

        Task<int> SetStatusAsync(int id, TaskStatusEditModel model);

        Task<int> SetProviderAsync(int id, TaskProviderEditModel model);
    }
}