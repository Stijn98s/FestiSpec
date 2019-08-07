using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class InputLengthRule : ValidationRule
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public InputLengthRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();


            if (checkedValue.Length >= Min && checkedValue.Length <= Max)
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, $"Moet uit minimaal {Min} en maximaal {Max} characters bestaan");
        }
    }
}
