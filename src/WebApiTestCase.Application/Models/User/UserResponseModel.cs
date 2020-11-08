using WebApiTestCase.Core.Enums;

namespace WebApiTestCase.Application.Models.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserStatus Status { get; set; }
    }
}