namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    public static class MdPostProviderHelper
    {
        public static bool HasH1 (string md)
        {
            var hashH1Finder = new Regex("(^#[^#])(.+$)", RegexOptions.Multiline);
            var underlinedH1Finder = new Regex("(^.+$)\n(^[=]+ *$)", RegexOptions.Multiline);

            return hashH1Finder.IsMatch(md) || underlinedH1Finder.IsMatch(md);
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
            {
                throw new ParseException($"Failed to find the end of the JSon preamble for {post.Date}-{post.Title} ");
            }

            var newlineAfterJson = md.IndexOf("\n", finalIndex + 1, StringComparison.Ordinal);
            var json = md.Substring(0, finalIndex + 1);
            var preamble = JsonSerializer.Deserialize<JsonPreamble>(json);
            post.Description = preamble.description;
            md = md.Substring(newlineAfterJson + 1);
        }
    }
}