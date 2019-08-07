using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsSubsequentDateValidationRule : ValidationRule
    {

        public IsSubsequentDateValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public static DateTime DateTime { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);

            var propertyName = expression?.ResolvedSourcePropertyName;
            var precheck = expression?.ResolvedSource.TryGetPropertyValue<DateTime>(propertyName);
            var checkedValue = DateTime.Now;

            if (precheck.Value != null)
            {
                checkedValue = precheck.Value;
            }

            if (checkedValue < DateTime.Date)
            {
                return new ValidationResult(false, "Datum moet na " + DateTime.ToString("d") + " zijn.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
