using FluentValidation;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.Validators.User;

namespace WebApiTestCase.Application.Models.Validators.Task
{
    public class TaskProviderEditModelValidator : AbstractValidator<TaskProviderEditModel>
    {
        public TaskProviderEditModelValidator()
        {
            RuleFor(m => m.ProviderId)
                .GreaterThanOrEqualTo(UserValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор пользователя-создателя должен быть больше либо равен {UserValidatorConfiguration.MinimumIdValue}");
        }
    }
}