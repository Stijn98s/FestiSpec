using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    internal class IsValidTitleValidationRule : ValidationRule
    {
        public IsValidTitleValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, @"^.{1,20}$"))
            {
                return new ValidationResult(false, "de ingevoerde waarde kan alleen uit letters en getallen bestaan");
            }

            return ValidationResult.ValidResult;
        }
    }
}
