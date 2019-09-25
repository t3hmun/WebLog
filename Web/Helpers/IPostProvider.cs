namespace t3hmun.WebLog.Web.Helpers
{
    using System.Threading.Tasks;

    public interface IPostProvider
    {
        Task<IPost> TryGetPost(string rawPostTitle);

        Task<IPostSummary[]> GetPostList();
    }
}