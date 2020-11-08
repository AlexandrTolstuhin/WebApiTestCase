using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCase.Application.Exceptions;
using WebApiTestCase.Application.Mappers;
using WebApiTestCase.Application.Models;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Services.Common;
using WebApiTestCase.Common.Collections;
using WebApiTestCase.Common.Extensions;
using WebApiTestCase.Data;
using WebApiTestCase.Data.Entities;

namespace WebApiTestCase.Application.Services.Task
{
    public class TaskService : ApplicationContextService, ITaskService
    {
        public TaskService(ApplicationContext context) : base(context)
        { }

        public async Task<int> CreateAsync(TaskCreateModel model)
        {
            var task = model.ToTaskEntity();

            await SaveChangesAsync();

            return task.Id;
        }

        public async Task<IPagedList<TaskResponseModel>> GetUserProvidedTasksAsync(int userId, PagedModel paged)
        {
            var pagedList = await Context.Tasks
                .AsNoTracking()
                .Where(t => t.ProviderId == userId)
                .Select(u => u.ToTaskResponseModel())
                .ToPagedListAsync(paged.CurrentPage, paged.PageSize);

            return pagedList;
        }

        public async Task<IPagedList<TaskResponseModel>> GetUserPerformedTasksAsync(int userId, PagedModel paged)
        {
            var pagedList = await Context.Tasks
                .AsNoTracking()
                .Where(t => t.PerformerId == userId)
                .Select(u => u.ToTaskResponseModel())
                .ToPagedListAsync(paged.CurrentPage, paged.PageSize);

            return pagedList;
        }

        public async Task<TaskResponseModel> GetTaskByIdAsync(int id)
        {
            var task = await GetTaskEntity(id);

            return task.ToTaskResponseModel();
        }

        public async Task<int> UpdateAsync(int id, TaskEditModel model)
        {
            var task = await GetTaskEntity(id);

            task.Name = model.Name;
            task.Description = model.Description;
            task.PerformerId = model.PerformerId;

            await SaveChangesAsync();

            return task.Id;
        }

        public async Task<int> SetStatusAsync(int id, TaskStatusEditModel model)
        {
            var task = await GetTaskEntity(id);

            task.Status = model.Status;

            await SaveChangesAsync();

            return task.Id;
        }

        public async Task<int> SetProviderAsync(int id, TaskProviderEditModel model)
        {
            var task = await GetTaskEntity(id);

            task.ProviderId = model.ProviderId;

            await SaveChangesAsync();

            return task.Id;
        }

        private async Task<TaskEntity> GetTaskEntity(int id)
        {
            var task = await Context.Tasks.FindAsync(id);

            if (task == null)
                throw new NotFoundException($"Задача с идентификатором {id} не найдена");
            return task;
        }
    }
}