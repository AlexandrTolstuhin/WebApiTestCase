using FluentValidation;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Application.Models.Validators.Task;

namespace WebApiTestCase.Application.Models.Validators.User
{
    public class UserAssignTaskModelValidator : AbstractValidator<UserAssignTaskModel>
    {
        public UserAssignTaskModelValidator()
        {
            RuleFor(m => m.ProviderId)
                .GreaterThanOrEqualTo(UserValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор пользователя-создателя должен быть больше либо равен {UserValidatorConfiguration.MinimumIdValue}");

            RuleFor(m => m.PerformerId)
                .GreaterThanOrEqualTo(UserValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор пользователя-исполнителя должен быть больше либо равен {UserValidatorConfiguration.MinimumIdValue}");

            RuleFor(m => m.TaskName)
                .MinimumLength(TaskValidatorConfiguration.MinimumNameLength)
                .WithMessage($"Название задачи должно иметь минимум {TaskValidatorConfiguration.MinimumNameLength} символов");
        }
    }
}