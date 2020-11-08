using WebApiTestCase.Core.Enums;

namespace WebApiTestCase.Application.Models.Task
{
    public class TaskResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public int ProviderId { get; set; }

        public int PerformerId { get; set; }
    }
}