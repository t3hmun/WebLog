namespace t3hmun.WebLog.Web.Models
{
    using System;
    using t3hmun.WebLog.Web.Helpers;

    public class MenuItem
    {
        public MenuItem(Type model)
        {
            Title = BaseModel.ModelName(model).CamelSpace();
            AspLink = $"/{BaseModel.ModelName(model)}";
        }

        public MenuItem(string title, string link)
        {
            Title = title;
            Link = link;
        }

        public string Title { get; }
        public string AspLink { get; set; }
        public string Link { get; set; }
    }
}