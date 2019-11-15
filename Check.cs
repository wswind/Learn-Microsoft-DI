using System;

namespace MicrosoftDI.Sample
{
    public static class Check
    {
        public static void AssertNotNull<T>(T value, string parameterName)
        {
            if (!NotNull(value))
                throw new ArgumentNullException(parameterName);
        }

        public static void AssertNotNull<T>(T value, string parameterName, string message)
        {
            if (!NotNull(value))
                throw new ArgumentNullException(parameterName, message);
        }

        public static void AssertNotEmpty(string value, string parameterName)
        {
            if (!NotEmpty(value))
                throw new ArgumentNullException(parameterName);
        }

        public static void AssertNotEmpty(string[] value, string parameterName)
        {
            if (!NotEmpty(value))
                throw new ArgumentNullException(parameterName);
        }

        public static bool NotNull<T>(T value)
        {
            return (value != null);
        }

        public static bool NotEmpty(string value)
        {
            return (!string.IsNullOrWhiteSpace(value));
        }

        public static bool NotEmpty(string[] value)
        {
            if (!NotNull(value) || value.Length == 0)
                return false;

            foreach (var v in value)
            {
                if (!NotEmpty(v))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
