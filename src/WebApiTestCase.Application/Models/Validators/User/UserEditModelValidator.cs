using FluentValidation;
using WebApiTestCase.Application.Models.User;

namespace WebApiTestCase.Application.Models.Validators.User
{
    public class UserEditModelValidator : AbstractValidator<UserEditModel>
    {
        public UserEditModelValidator()
        {
            RuleFor(m => m.FirstName)
                .MinimumLength(UserValidatorConfiguration.MinimumFirstNameLength)
                .WithMessage($"Имя должно иметь минимум {UserValidatorConfiguration.MinimumFirstNameLength} символов");

            RuleFor(m => m.LastName)
                .MinimumLength(UserValidatorConfiguration.MinimumLastNameLength)
                .WithMessage($"Фамилия должна иметь минимум {UserValidatorConfiguration.MinimumLastNameLength} символов");
        }
    }
}