using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    internal class IsPastDateValidationRule : ValidationRule
    {
        private readonly DateTime _today = DateTime.Today;

        public IsPastDateValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var precheck = expression?.ResolvedSource.TryGetPropertyValue<DateTime>(propertyName);
            var checkedValue = precheck.Value;

            if (checkedValue >= _today.Date)
            {
                return new ValidationResult(false, $"Er moet een datum ingevoerd worden voor {_today.ToString("d")}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
