using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApiTestCase.Application.Models.Task;
using WebApiTestCase.Application.Models.Validators.Task;

namespace WebApiTestCase.Application.Test.ModelValidators.TaskValidators
{
    public class TaskEditModelValidatorTest
    {
        private TaskEditModelValidator _validator;

        private TaskEditModel _validModel;

        [SetUp]
        public void Setup()
        {
            _validator = new TaskEditModelValidator();

            _validModel = new TaskEditModel
            {
                Name = "Task Name",
                Description = "Description",
                PerformerId = 1
            };
        }

        [Test]
        public void Validator_Should_Have_Error_When_Name_Length_Less_Than_Five()
        {
            var model = _validModel;
            model.Name = "Four";

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Name);
        }

        [Test]
        public void Validator_Should_Have_Error_When_PerformerId_Less_Than_One()
        {
            var model = _validModel;
            model.PerformerId = 0;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.PerformerId);
        }

        [Test]
        public void Validator_Should_Not_Have_Error_When_Description_Are_Null()
        {
            var model = _validModel;
            model.Description = null;

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            var model = _validModel;

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}