using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Data.Entities;

namespace WebApiTestCase.Application.Mappers
{
    public static class UserMappers
    {
        public static UserResponseModel ToUserResponseModel(this UserEntity entity)
        {
            return new UserResponseModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Status = entity.Status
            };
        }
    }
}