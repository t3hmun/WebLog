namespace t3hmun.WebLog.Web.Models
{
    using System;

    public interface ILayout
    {
        string Title { get; }
        Type[] Menu { get; }
    }
}