using FestiDB.Domain;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using FestiDB.Domain.Roles;

namespace FestiApp.Util
{
    public class ValidationHelper
    {
        public static bool IsBetweenLength(int v1, int v2, string firstName)
        {
            return firstName.Length >= v2 && firstName.Length <= v1;
        }

        public static bool IsSubsequentDate(DateTime startDate, DateTime endDate)
        {
            return (endDate >= startDate);
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,30}$");

        }

        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsEmpty(DateTime value)
        {
            return value.ToString(CultureInfo.InvariantCulture).Length != 0;
        }

        public static bool IsEmpty(Gender value)
        {
            return value.ToString().Length != 0;
        }

        public static bool IsEmpty(Role value)
        {
            return value.ToString().Length != 0;
        }

        public static bool IsCharacterOnly(string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z\s]*$");
        }

        public static bool IsPastDate(DateTime date)
        {
            return date < DateTime.Today;
        }

        public static bool IsPhoneNumber(string value)
        {
            return Regex.IsMatch(value, @"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)");
        }

        public static bool IsKVKNumber(string value)
        {
            return Regex.IsMatch(value, "^[0-9]{8}$");
        }

        public static bool IsEmail(string value)
        {
            return Regex.IsMatch(value,
                @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" +
                @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        }

        public static bool IsHouseNumber(string value)
        {
            return Regex.IsMatch(value, @"^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$");
        }

        public static bool IsPostalCode(string value)
        {
            return Regex.IsMatch(value, @"^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$");
        }

        public static bool IsValidTitle(string value)
        {
            return Regex.IsMatch(value, @"^.{1,20}$");
        }

        public static bool IsSamePassword(string value, string match)
        {
            return value.Equals(match);
        }

        public static bool IsEmptyCustomer(Customer value)
        {
            return value.ToString().Length != 0;
        }
    }
}