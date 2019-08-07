using NJsonSchema.Infrastructure;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsEmptyValidationRule : ValidationRule
    {

        public IsEmptyValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var precheck = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            var checkedValue = precheck;

            if (string.IsNullOrEmpty(checkedValue))
            {
                return new ValidationResult(false, "Voer een waarde in");
            }

            return ValidationResult.ValidResult;
        }
    }
}
