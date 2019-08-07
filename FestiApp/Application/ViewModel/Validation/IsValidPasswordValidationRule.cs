using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using NJsonSchema.Infrastructure;

namespace FestiApp.ViewModel.Validation
{
    public class IsValidPasswordValidationRule : ValidationRule
    {

        public IsValidPasswordValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<string>(propertyName);
            if (checkedValue == null) throw new InvalidOperationException();

            if (!Regex.IsMatch(checkedValue, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,30}$"))
            {
                return new ValidationResult(false,
                    "Het wachtwoord moet tussen 8-30 karakters,\n1 getal, 1 speciaal teken,\n1 hoofdletter en 1 kleine letter  bevatten");
            }

            return ValidationResult.ValidResult;
        }
    }
}
