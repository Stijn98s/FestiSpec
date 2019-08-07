using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsValidPostalCodeValidationRule : ValidationRule
    {
        public IsValidPostalCodeValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, @"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$"))
            {
                return new ValidationResult(false, "Ongeldige postcode");
            }

            return ValidationResult.ValidResult;
        }
    }
}
