using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsEmptyDateTimeValidationRule : ValidationRule
    {

        public IsEmptyDateTimeValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;

            var precheck = expression?.ResolvedSource.TryGetPropertyValue<DateTime>(propertyName);
            var checkedValue = precheck.Value;


            if (checkedValue == null)
            {
                return new ValidationResult(false, "Voer een waarde in");
            }

            return ValidationResult.ValidResult;
        }
    }
}
