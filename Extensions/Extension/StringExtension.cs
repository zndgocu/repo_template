using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Extension
{
    public static class StringExtension
    {
        public static string GetUnique(this string str)
        {
            return $"str{Guid.NewGuid().ToString()}";
        }
        public static string Quot(this string str)
        {
            return $"'{str}'";
        }
        public static string? UpperFirstChar(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return char.ToUpper(text[0]) + text.Substring(1);
        }
        public static string? LowerFirstChar(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return char.ToLower(text[0]) + text.Substring(1);
        }

        public static string ToCamelCase(this string text)
        {
            var pascalCase = text.ToPascalCase();
            return pascalCase.ToLower();

        }
        public static string ToPascalCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return string.Join("", text.Split('_')
                         .Select(w => w.Trim())
                         .Where(w => w.Length > 0)
                         .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
        }
        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}