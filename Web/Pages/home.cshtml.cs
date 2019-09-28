namespace t3hmun.WebLog.Web.Pages
{
    using System.Linq;
    using System.Threading.Tasks;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Models;

    public class HomeModel : BaseModel
    {
        private readonly IPostProvider _postProvider;
        public IPostSummary[] PostSummaries { get; set; }
        public string MessageOfTheDay { get; set; } = "TODO: MOTD";
        public HomeModel(IPostProvider postProvider)
        {
            _postProvider = postProvider;
            Title = "";
        }

        public async Task OnGet()
        {
            PostSummaries = await _postProvider.GetPostList().ToArrayAsync();
        }
    }
}