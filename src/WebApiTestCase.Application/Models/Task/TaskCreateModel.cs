namespace WebApiTestCase.Application.Models.Task
{
    public class TaskCreateModel
    {
        public int ProviderId { get; set; }

        public int PerformerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}