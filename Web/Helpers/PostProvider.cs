namespace t3hmun.WebLog.Web.Helpers
{
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

        public async Task<(bool exisits, string html)> TryGetPost(string rawPostTitle)
        {
            var file = _fileProvider.GetFileInfo($"md/{rawPostTitle}.md");
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

                return (true, Parse(md));
            }

            return (false, null);
        }

        private string Parse(string markdown)
        {
            var html = Markdown.ToHtml(markdown, _pipeline);
            //TODO: Link parser to replace ~/ links between posts.
            return html;
        }
    }
}