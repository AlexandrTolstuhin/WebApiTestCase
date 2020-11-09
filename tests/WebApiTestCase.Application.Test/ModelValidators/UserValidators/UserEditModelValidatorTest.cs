using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApiTestCase.Application.Models.User;
using WebApiTestCase.Application.Models.Validators.User;

namespace WebApiTestCase.Application.Test.ModelValidators.UserValidators
{
    public class UserEditModelValidatorTest
    {
        private UserEditModelValidator _validator;

        private UserEditModel _validModel;

        [SetUp]
        public void Setup()
        {
            _validator = new UserEditModelValidator();

            _validModel = new UserEditModel
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };
        }

        [Test]
        public void Validator_Should_Have_Error_When_FirstName_Length_Less_Than_Two()
        {
            var model = _validModel;
            model.FirstName = "1";

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.FirstName);
        }

        [Test]
        public void Validator_Should_Have_Error_When_LastName_Length_Less_Than_Two()
        {
            var model = _validModel;
            model.LastName = "1";

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.LastName);
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