using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApiTestCase.Application.Models;

namespace WebApiTestCase.Application.Test.ModelValidators.PagedModelValidator
{
    public class PagedModelValidatorTest
    {
        private Models.Validators.Paged.PagedModelValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Models.Validators.Paged.PagedModelValidator();
        }

        [Test]
        public void Validator_Should_Have_Error_When_CurrentPage_Less_Than_Zero()
        {
            var model = new PagedModel { CurrentPage = -1 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.CurrentPage);
        }

        [Test]
        public void Validator_Should_Have_Error_When_CurrentPage_Less_Than_One()
        {
            var model = new PagedModel { PageSize = -1 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.PageSize);
        }

        [Test]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Have_Default_Values()
        {
            var model = new PagedModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            var model = new PagedModel { CurrentPage = 42, PageSize = 100 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}