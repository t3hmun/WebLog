namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    public interface IPostSummary
    {
        string Title { get; }
        DateTime Date { get; }
        string Description { get; set; }
        string Link { get; set; }
    }
}