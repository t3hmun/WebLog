namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Markdig;
    using Microsoft.Extensions.FileProviders;

    public class MdPostProvider : IPostProvider
    {
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
            var file = _fileProvider.GetFileInfo($"md/post/{rawPostTitle}.md");
            if (!file.Exists) throw new PostDoesNotExistException($"title: {rawPostTitle}");

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
                post.H1IsMissing = MdPostProviderHelper.HasH1(post.Html);
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
            using (var stream = file.CreateReadStream())
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
            var rawTitle = rawPostTitle.Substring(7);
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