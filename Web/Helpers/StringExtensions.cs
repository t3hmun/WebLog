namespace t3hmun.WLog.Web.Helpers
{
    using System.Collections.Generic;
    public static class StringExtensions
    {
        public static string CamelSpace(this string camelText)
        {
            bool previousIsUpper = false;
            List<char> camelSpaced = new List<char>();
            foreach(char letter in camelText)
            {
                if(char.IsUpper(letter))
                {
                    if(previousIsUpper) camelSpaced.Add(' ');
                    previousIsUpper = true;
                }
                else 
                {
                    previousIsUpper = false;
                }
                camelSpaced.Add(letter);
            }

            return new string(camelSpaced.ToArray());
        }
    }
}