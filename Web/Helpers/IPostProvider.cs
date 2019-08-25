namespace t3hmun.WebLog.Web.Helpers
{
    using System.Threading.Tasks;

    public interface IPostProvider
    {
        Task<(bool exisits, string html)> TryGetPost(string rawPostTitle);
    }
}