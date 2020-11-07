namespace WebApiTestCase.Application.Models.Task
{
    public class TaskEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PerformerId { get; set; }
    }
}