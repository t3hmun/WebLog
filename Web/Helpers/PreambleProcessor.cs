namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Text.Json;
    using JetBrains.Annotations;

    public class PreambleProcessor
    {
        public static void JsonPreamble([NotNull] ref string md, [NotNull] IPost post)
        {
            if (md.StartsWith("{"))
            {
                var endOfFirstLine = md.IndexOf("\n", StringComparison.Ordinal);
                var json = md.Substring(0, endOfFirstLine);
                var preamble = JsonSerializer.Deserialize<JsonPreamble>(json);
                post.Description = preamble.description;
                md = md.Substring(endOfFirstLine + 1);
            }
        }
    }
}