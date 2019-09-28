namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    public static class MdPostProviderHelper
    {
        private static readonly Regex H1InHtml =
            new Regex(@"<h1>(.*?)</h1>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private static readonly Regex HtmlTags = new Regex(@"<.*?>");

        /// <summary>
        ///     Extracts plaintext content of the first H1 tag if it exists, otherwise null.
        /// </summary>
        /// <param name="html">The HTML of the file to find H1 in.</param>
        /// <returns>The plaintext of the </returns>
        public static string GetH1FromHtml(string html)
        {
            var match = H1InHtml.Match(html);

            if (match.Success)
            {
                var h1Html = match.Groups[1].Value;
                // I don't want to keep any html formatting, trying to transplant text with formatting is madness.
                var stripHtml = HtmlTags.Replace(h1Html, "");
                var decoded = WebUtility.HtmlDecode(stripHtml);
                return decoded;
            }

            return null;
        }


        public static void ParseAndRemoveJsonPreamble([NotNull] ref string md, [NotNull] IPost post)
        {
            if (!md.StartsWith("{")) return;

            var prev = md[0];
            var openCount = 1;
            var closeCount = 0;
            var finalIndex = 0;
            for (var i = 1; i < md.Length; i++)
            {
                var current = md[i];
                if (current == '{' && prev != '\\') openCount++;
                else if (current == '}' && prev != '\\') closeCount++;
                if (openCount == closeCount)
                {
                    finalIndex = i;
                    break;
                }

                prev = current;
            }

            if (finalIndex == 0)
                throw new ParseException($"Failed to find the end of the JSon preamble for {post.Date}-{post.Title} ");

            var newlineAfterJson = md.IndexOf("\n", finalIndex + 1, StringComparison.Ordinal);
            var json = md.Substring(0, finalIndex + 1);
            var preamble = JsonSerializer.Deserialize<JsonPreamble>(json);
            post.Description = preamble.description;
            md = md.Substring(newlineAfterJson + 1);
        }
    }
}