namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Markdig;
    using Microsoft.Extensions.FileProviders;

    public class PostProvider : IPostProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly MarkdownPipeline _pipeline;

        public PostProvider(IFileProvider fileProvider)
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
            var file = _fileProvider.GetFileInfo($"md/{rawPostTitle}.md");
            var post = new Post
            {
                Title = ExtractTitle(rawPostTitle),
                Date = ExtractDate(rawPostTitle),
                Html = null,
                Errors = null
            };
            if (file.Exists)
            {
                string md;
                using (var stream = file.CreateReadStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        md = await sr.ReadToEndAsync();
                    }
                }

                try
                {
                    post.Html = Parse(md);
                }
                catch (Exception e)
                {
                    //TODO: LOG errors.
                    post.Errors = e.Message;
                }
            }

            return post;
        }

        private static string ExtractTitle(string rawPostTitle)
        {
            var rawTitle = rawPostTitle.Substring(7);
            var withSpaces = rawTitle.Replace("_", " ").Replace("  ", " _");
            return withSpaces;
        }

        private static DateTime ExtractDate(string rawPostTitle)
        {
            return DateTime.ParseExact(rawPostTitle.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
        }

        private string Parse(string markdown)
        {
            var html = Markdown.ToHtml(markdown, _pipeline);
            //TODO: Link parser to replace ~/ links between posts.
            return html;
        }
    }
}