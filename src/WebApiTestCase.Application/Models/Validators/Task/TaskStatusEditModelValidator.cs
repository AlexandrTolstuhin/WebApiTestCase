using FluentValidation;
using WebApiTestCase.Application.Models.Task;

namespace WebApiTestCase.Application.Models.Validators.Task
{
    public class TaskStatusEditModelValidator : AbstractValidator<TaskStatusEditModel>
    {
        public TaskStatusEditModelValidator()
        {
            RuleFor(m => m.Id)
                .GreaterThanOrEqualTo(TaskValidatorConfiguration.MinimumIdValue)
                .WithMessage($"Идентификатор задачи должен быть больше либо равен {TaskValidatorConfiguration.MinimumIdValue}");
        }
    }
}