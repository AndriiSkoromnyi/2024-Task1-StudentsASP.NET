using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.ValidationAttributes
{
    public class PolishPostalCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Null values are considered valid.

            string postalCode = value.ToString();

            Regex regex = new Regex(@"^\d{2}-\d{3}$");

            return regex.IsMatch(postalCode);
        }
    }
}
