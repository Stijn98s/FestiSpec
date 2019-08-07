using NJsonSchema.Infrastructure;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace FestiApp.ViewModel.Validation
{
    public class IsPhoneNumberValidationRule : ValidationRule
    {
        public IsPhoneNumberValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, @"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)"))
            {
                return new ValidationResult(false, "Telefoonnummer is niet geldig" + " \n 0000-000000, +31 6 0000 0000, 06 0000 0000 ");
            }

            return ValidationResult.ValidResult;
        }
    }
}
