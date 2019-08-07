using FestiDB.Domain;
using NJsonSchema.Infrastructure;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using FestiApp.ViewModel.Customers;

namespace FestiApp.ViewModel.Validation
{
    internal class IsEmptyCustomerValidationRule : ValidationRule
    {

        public IsEmptyCustomerValidationRule()
        {
            ValidationStep = ValidationStep.UpdatedValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var expression = value as BindingExpression;
            var propertyName = expression?.ResolvedSourcePropertyName;
            var checkedValue = expression?.ResolvedSource.TryGetPropertyValue<CustomerViewModel>(propertyName);

            if (checkedValue == null)
            {
                return new ValidationResult(false, "Selecteer een klant");
            }

            return ValidationResult.ValidResult;
        }
    }
}
