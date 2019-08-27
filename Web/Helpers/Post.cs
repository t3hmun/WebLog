namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    internal class Post : IPost
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Html { get; set; }
        public bool Exists => Html != null;
        public string Errors { get; set; }
        public string Description { get; set; }
        public bool H1IsMissing { get; set; }
    }
}