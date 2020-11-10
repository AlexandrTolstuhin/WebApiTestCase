using FluentValidation;

namespace WebApiTestCase.Application.Models.Validators.Paged
{
    public class PagedModelValidator : AbstractValidator<PagedModel>
    {
        public PagedModelValidator()
        {
            RuleFor(m => m.CurrentPage)
                .GreaterThanOrEqualTo(PagedModelValidatorConfiguration.MinimumCurrentPage)
                .WithMessage($"Индекс текущей страницы должен быть больше либо равен {PagedModelValidatorConfiguration.MinimumCurrentPage}");

            RuleFor(m => m.PageSize)
                .GreaterThanOrEqualTo(PagedModelValidatorConfiguration.MinimumPageSize)
                .WithMessage($"Размер страницы должен быть больше либо равен {PagedModelValidatorConfiguration.MinimumPageSize}");
        }
    }
}