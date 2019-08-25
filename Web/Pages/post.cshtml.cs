namespace t3hmun.WebLog.Web.Pages
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Models;

    public class PostModel : BaseModel
    {
        public const string RawTitleRouteString = "page-title";
        private readonly IPostProvider _postProvider;

        public PostModel(IPostProvider postProvider)
        {
            _postProvider = postProvider;
        }

        public IPost Post { get; private set; }


        [UsedImplicitly]
        public async Task OnGet()
        {
            var rawPageTitle = RouteData.Values[RawTitleRouteString].ToString();
            Post = await _postProvider.TryGetPost(rawPageTitle);
        }
    }
}