using FluentValidation;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.Validators.User;

namespace WebApiTestCase.Application.Models.Validators.Task
{
    public class TaskEditModelValidator : AbstractValidator<TaskEditModel>
    {
        public TaskEditModelValidator()
        {
            RuleFor(m => m.PerformerId)
                .GreaterThanOrEqualTo(UserValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор пользователя-исполнителя должен быть больше либо равен {UserValidatorConfiguration.MinimumIdValue}");

            RuleFor(m => m.Name)
                .MinimumLength(TaskValidatorConfiguration.MinimumNameLength)
                .WithMessage($"Название задачи должно иметь минимум {TaskValidatorConfiguration.MinimumNameLength} символов");
        }
    }
}