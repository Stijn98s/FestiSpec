using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsValidHouseNumber : ValidationRule
    {
        public IsValidHouseNumber()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, @"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$"))
            {
                return new ValidationResult(false, "Het ingevoerde huisnummer is ongeldig");
            }

            return ValidationResult.ValidResult;
        }
    }
}
