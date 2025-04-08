using System;

namespace PROG2500_A2_Chinook.Helpers
{
    public static class StringHelper
    {
        public static string GetFirstCharacter(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty", nameof(input));

            return input.Substring(0, 1).ToUpper();
        }

        public static string FormatName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            // First name can be null or empty
            string formattedFirstName = string.IsNullOrEmpty(firstName) ? "" : firstName;

            return $"{lastName}, {formattedFirstName}".Trim(',', ' ');
        }

        public static string FormatLocationInfo(string city, string state)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("City cannot be null or empty", nameof(city));

            if (string.IsNullOrEmpty(state))
                return city;

            return $"{city}, {state}";
        }

        public static bool ContainsIgnoreCase(string source, string toCheck)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(toCheck);

            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}