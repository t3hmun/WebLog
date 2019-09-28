namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Markdig;
    using Microsoft.Extensions.FileProviders;

    /// <remarks>
    ///     There is 0 caching in this class. This is designed to be maintainable, not efficient.
    ///     Caching can be implemented at a much higher level, a separate responsibility, this code remains ignorant.
    ///     As a result of caching this code won't run enough for a few ms of efficiency to matter at all.
    /// </remarks>
    public class MdPostProvider : IPostProvider
    {
        private const string MdDir = "md/post/";
        public static readonly Regex ValidFilenames = new Regex(@".*\d\d\d\d-\d\d-\d\d-.*\.md", RegexOptions.IgnoreCase);
        private readonly IFileProvider _fileProvider;
        private readonly MarkdownPipeline _pipeline;

        public MdPostProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            var builder = new MarkdownPipelineBuilder();
            _pipeline = builder
                .UseAutoLinks()
                .UseDefinitionLists()
                .UseDiagrams()
                .UseEmphasisExtras()
                .UseListExtras()
                .UseCustomContainers()
                .Build();
        }

        public async Task<IPost> TryGetPost(string rawPostTitle)
        {
            var file = _fileProvider.GetFileInfo($"{MdDir}{rawPostTitle}.md");
            if (!file.Exists) throw new PostDoesNotExistException($"title: {rawPostTitle}");

            var post = await ReadAndParsePost(rawPostTitle, file);

            return post;
        }

        public async IAsyncEnumerable<IPostSummary> GetPostList()
        {
            var files = _fileProvider.GetDirectoryContents(MdDir);
            var validFilenames = ValidFilenames;
            foreach (var file in files)
            {
                if (!validFilenames.IsMatch(file.Name)) continue;
                // These files are never big, the mental overhead of only reading the summary is not worth it.

                var rawPostTitle = file.Name;
                var post = await ReadAndParsePost(rawPostTitle, file);
                yield return post;
            }
        }

        private async Task<Post> ReadAndParsePost(string rawPostTitle, IFileInfo file)
        {
            var post = new Post
            {
                Title = ExtractTitle(rawPostTitle),
                Date = ExtractDate(rawPostTitle),
                Html = null,
                Errors = null
            };
            var rawFile = await ReadFile(file);

            var md = rawFile;
            MdPostProviderHelper.ParseAndRemoveJsonPreamble(ref md, post);

            try
            {
                post.Html = Parse(md);
                post.H1IsMissing = !MdPostProviderHelper.HasH1(post.Html);
            }
            catch (Exception e)
            {
                //TODO: LOG errors.
                //TODO: This should probably throw and fall through to a generic error?
                post.Errors = e.Message;
            }

            return post;
        }


        private static async Task<string> ReadFile(IFileInfo file)
        {
            string md;
            await using (var stream = file.CreateReadStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    md = await sr.ReadToEndAsync();
                }
            }

            return md;
        }

        private static string ExtractTitle(string rawPostTitle)
        {
            var rawTitle = rawPostTitle.Substring(11);
            var withSpaces = rawTitle.Replace("_", " ").Replace("  ", " _");
            return withSpaces;
        }

        private static DateTime ExtractDate(string rawPostTitle)
        {
            var dateBit = rawPostTitle.Substring(0, 10);
            return DateTime.ParseExact(dateBit, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private string Parse(string markdown)
        {
            var html = Markdown.ToHtml(markdown, _pipeline);
            //TODO: Link parser to replace ~/ links between posts.
            return html;
        }
    }
}