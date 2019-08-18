namespace t3hmun.WLog.Web.Model
{
    using System;
    public class MenuItem
    {
        public string Title { get; }
        public string AspLink { get; set; }

        public MenuItem(Type model)
        {
            Title = BaseModel.DefaultTitle(model);
            AspLink = $"/{BaseModel.ModelName(model)}";
        }
    }
}