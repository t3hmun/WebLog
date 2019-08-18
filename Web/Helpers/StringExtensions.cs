namespace t3hmun.WLog.Web.Helpers
{
    using System.Collections.Generic;

    public static class StringExtensions
    {
        ///<summary>Add spaces before capital letters except for in acronyms, next to punctuation or existing preceding whitespace. 
        /// Numbers are treated like lower case letters.
        /// Never adds spaces on any side of punctuation.</summary>
        public static string CamelSpace(this string camelText)
        {
            if (camelText.Length <= 1) return camelText;

            List<char> result = new List<char>();
            result.Add(camelText[0]);

            for (int i = 1; i < camelText.Length; i++)
            {
                var current = camelText[i];
                var previous = camelText[i - 1];

                if (char.IsUpper(current))
                {
                    if (char.IsLower(previous) || char.IsDigit(previous))
                    {
                        result.Add(' ');
                    }
                    else if (i != camelText.Length - 1 && char.IsUpper(previous))
                    {
                        var next = camelText[i + 1];
                        if (char.IsLower(next) || char.IsDigit(next)) result.Add(' ');
                    }
                }

                result.Add(current);
            }

            return new string(result.ToArray());
        }
    }
}