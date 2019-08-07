using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsSamePasswordValidationRule : ValidationRule
    {
        public static string Password
        {
            get;
            set;
        }

        public IsSamePasswordValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!checkedValue.Equals(Password))
            {
                return new ValidationResult(false, "De twee wachtwoorden zijn niet gelijk.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
