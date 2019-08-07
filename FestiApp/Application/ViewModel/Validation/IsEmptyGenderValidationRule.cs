using FestiDB.Domain;
using NJsonSchema.Infrastructure;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    internal class IsEmptyGenderValidationRule : ValidationRule
    {

        public IsEmptyGenderValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<Gender>(propertyName);

            if (checkedValue == null)
            {
                return new ValidationResult(false, "Selecteer uw gender");
            }

            return ValidationResult.ValidResult;
        }
    }
}
