namespace t3hmun.WebLog.Web.Models
{
    using System;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Pages;

    public abstract class BaseModel : PageModel
    {
        public BaseModel()
        {
            Title = DefaultTitle(GetType());
            Menu = new[]
            {
                new MenuItem(typeof(HomeModel)),
                new MenuItem(typeof(ErrorModel)),
                new MenuItem("GitHub", "https://wwww.github.com/t3hmun")
            };
        }

        public string Title { get; protected set; }

        public MenuItem[] Menu { get; }

        public static string ModelName(Type model)
        {
            var name = model.Name;
            if (name.EndsWith("Model")) name = name.Substring(0, name.Length - 5);
            return name;
        }

        public static string DefaultTitle(Type model)
        {
            return ModelName(model).CamelSpace();
        }
    }
}