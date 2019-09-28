namespace t3hmun.WebLog.Web.Pages
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Models;

    public class PostModel : BaseModel
    {
        /// <summary>Base route for generating links to posts - used in server route setup.</summary>
        public const string RouteBase = "Post";

        /// <summary>Var name of the post title segment of the route - used in server route setup.</summary>
        public const string RawTitleRoute = "page-title";

        private readonly IPostProvider _postProvider;

        public PostModel(IPostProvider postProvider)
        {
            _postProvider = postProvider;
        }

        public IPost Post { get; private set; }


        [UsedImplicitly]
        public async Task OnGet()
        {
            var rawPageTitle = RouteData.Values[RawTitleRoute].ToString();
            Post = await _postProvider.TryGetPost(rawPageTitle);
        }
    }
}