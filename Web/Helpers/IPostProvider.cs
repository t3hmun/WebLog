namespace t3hmun.WebLog.Web.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostProvider
    {
        Task<IPost> TryGetPost(string rawPostTitle);

        IAsyncEnumerable<IPostSummary> GetPostList();
    }
}