namespace t3hmun.WebLog.Web.Pages
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.FileProviders;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Models;

    public class PostModel : BaseModel
    {
        public const string RawTitleRouteString = "page-title";
        private readonly IFileProvider _fileProvider;
        private readonly IPostParser _postParser;

        public PostModel(IFileProvider fileProvider, IPostParser postParser)
        {
            _fileProvider = fileProvider;
            _postParser = postParser;
        }

        public string PostHtml { get; private set; }
        public bool Exists { get; private set; }

        public async Task OnGet()
        {
            var rawPageTitle = RouteData.Values[RawTitleRouteString];
            //TODO: Move all this file stuff into IPostProvider.
            var file = _fileProvider.GetFileInfo($"md/{rawPageTitle}.md");
            Exists = file.Exists;
            if (Exists)
            {
                var stream = file.CreateReadStream();
                string md;
                using (stream)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        md = await sr.ReadToEndAsync();
                    }
                }

                var html = _postParser.Parse(md);
                PostHtml = html;
            }
        }
    }
}