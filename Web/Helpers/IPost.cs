namespace t3hmun.WebLog.Web.Helpers
{
    using JetBrains.Annotations;

    public interface IPost : IPostSummary
    {

        [CanBeNull] string Html { get; }

        bool Exists { get; }

        string Errors { get; }
        bool H1IsMissing { get; set; }
    }
}