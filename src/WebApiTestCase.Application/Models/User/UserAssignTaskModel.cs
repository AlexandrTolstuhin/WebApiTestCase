namespace WebApiTestCase.Application.Models.User
{
    public class UserAssignTaskModel
    {
        public int ProviderId { get; set; }

        public int PerformerId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }
    }
}