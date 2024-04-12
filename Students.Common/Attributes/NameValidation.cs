using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Null values are considered valid.

            string name = value as string;
            if (string.IsNullOrWhiteSpace(name))
                return false; // Empty or whitespace-only names are not valid.

            Regex regex = new Regex(@"^[A-Z][a-z]* [A-Z][a-z]*$");

            return regex.IsMatch(name);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Name must start with a capital letter, have only one space, and not contain digits.";
        }
    }
}
