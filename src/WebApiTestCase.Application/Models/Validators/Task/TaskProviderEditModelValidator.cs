using FluentValidation;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.Validators.User;
using WebApiTestCase.Data;

namespace WebApiTestCase.Application.Models.Validators.Task
{
    public class TaskProviderEditModelValidator : AbstractValidator<TaskProviderEditModel>
    {
        private readonly ApplicationContext _context;

        public TaskProviderEditModelValidator(ApplicationContext context)
        {
            _context = context;

            RuleFor(m => m.Id)
                .GreaterThanOrEqualTo(TaskValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор задачи должен быть больше либо равен {TaskValidatorConfiguration.MinimumIdValue}");

            RuleFor(m => m.ProviderId)
                .GreaterThanOrEqualTo(UserValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор пользователя-создателя должен быть больше либо равен {UserValidatorConfiguration.MinimumIdValue}");
        }
    }
}