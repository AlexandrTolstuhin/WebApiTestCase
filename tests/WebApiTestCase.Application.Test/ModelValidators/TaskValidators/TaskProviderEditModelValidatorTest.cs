using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.Validators.Task;

namespace WebApiTestCase.Application.Test.ModelValidators.TaskValidators
{
    public class TaskProviderEditModelValidatorTest
    {
        private TaskProviderEditModelValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new TaskProviderEditModelValidator();
        }

        [Test]
        public void Validator_Should_Have_Error_When_ProviderId_Less_Than_One()
        {
            var model = new TaskProviderEditModel { ProviderId = 0 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.ProviderId);
        }

        [Test]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            var model = new TaskProviderEditModel { ProviderId = 42 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}