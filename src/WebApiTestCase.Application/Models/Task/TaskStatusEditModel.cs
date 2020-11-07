using WebApiTestCase.Core.Enums;

namespace WebApiTestCase.Application.Models.Task
{
    public class TaskStatusEditModel
    {
        public int Id { get; set; }

        public TaskStatus Status { get; set; }
    }
}