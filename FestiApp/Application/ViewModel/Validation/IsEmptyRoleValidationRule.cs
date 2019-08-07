using FestiDB.Domain;
using NJsonSchema.Infrastructure;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using FestiDB.Domain.Roles;

namespace FestiApp.ViewModel.Validation
{
    internal class IsEmptyRoleValidationRule : ValidationRule
    {

        public IsEmptyRoleValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = (value as BindingExpression);
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<Role>(propertyName);

            if (checkedValue == null)
            {
                return new ValidationResult(false, "Selecteer een rol");
            }

            return ValidationResult.ValidResult;
        }
    }
}
