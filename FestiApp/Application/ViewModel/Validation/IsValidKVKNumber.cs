using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsValidKVKNumber : ValidationRule
    {
        public IsValidKVKNumber()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);

            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, "^[0-9]{8}$"))
            {
                return new ValidationResult(false, "KVK nummer is niet geldig" + " \n 12345678 of 09876543 ");
            }

            return ValidationResult.ValidResult;
        }
    }
}

