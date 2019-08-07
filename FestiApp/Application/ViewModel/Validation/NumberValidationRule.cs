using NJsonSchema.Infrastructure;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class NumberValidationRule : ValidationRule
    {
        public NumberValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<int>(propertyName);
            if (checkedValue == null) return new ValidationResult(false, "Voer een nummer in");

            if (!(checkedValue <= Max && checkedValue >= Min))
            {
                return new ValidationResult(false, $"Nummer moet tussen {Min.ToString()} en {Max.ToString()} zijn");
            }

            return ValidationResult.ValidResult;
        }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 10;
    }
}