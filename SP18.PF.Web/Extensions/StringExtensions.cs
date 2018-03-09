using System;

namespace SP18.PF.Web.Extensions
{
    public static class StringExtensions
    {
        public static int SafeParseInt(this string value)
        {
            if (int.TryParse(value ?? string.Empty, out var result))
            {
                return result;
            }
            return -1;
        }

        public static int[] SafeParseIntTokens(this string value, int size)
        {
            var tokens = (value ?? string.Empty).Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new int[size];
            for (var index = 0; index < result.Length; index++)
            {

                if (index < tokens.Length)
                {
                    result[index] = SafeParseInt(tokens[index]);
                }
                else
                {
                    result[index] = -1;
                }
            }
            return result;
        }
    }
}