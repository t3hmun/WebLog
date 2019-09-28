namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    public class Post : IPost
    {
        public bool H1IsMissing { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Html { get; set; }
        public bool Exists => Html != null;
        public string Errors { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}