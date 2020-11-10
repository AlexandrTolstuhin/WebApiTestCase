using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Core.Enums;
using WebApiTestCase.Data.Entities;

namespace WebApiTestCase.Application.Mappers
{
    public static class TaskMappers
    {
        public static TaskResponseModel ToTaskResponseModel(this TaskEntity entity)
        {
            return new TaskResponseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ProviderId = entity.ProviderId,
                PerformerId = entity.PerformerId,
                Status = entity.Status
            };
        }

        public static TaskEntity ToTaskEntity(this TaskCreateModel model)
        {
            return new TaskEntity
            {
                Name = model.Name,
                Description = model.Description,
                ProviderId = model.ProviderId,
                PerformerId = model.PerformerId,
                Status = TaskStatus.NotStarted
            };
        }
    }
}