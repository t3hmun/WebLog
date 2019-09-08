namespace t3hmun.WebLog.Web.Helpers
{
    using System;
    using JetBrains.Annotations;

    public interface IPost
    {
        string Title { get; }
        DateTime Date { get; }

        [CanBeNull] string Html { get; }

        bool Exists { get; }

        string Errors { get; }
        string Description { get; set; }
    }
}