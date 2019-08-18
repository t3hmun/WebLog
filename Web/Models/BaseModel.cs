namespace t3hmun.WLog.Web.Model
{
    using System;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using t3hmun.WLog.Web.Helpers;
    using t3hmun.WLog.Web.Pages;
    
    public abstract class BaseModel : PageModel
    {
        public string Title { get; protected set; }

        public MenuItem[] Menu { get;}

        public BaseModel()
        {
            Title = DefaultTitle(GetType());
            Menu = new MenuItem[]{
                new MenuItem(typeof(HomeModel))
            };
        }

        public static string ModelName(Type model)
        {
            var name = model.Name;
            if(name.EndsWith("Model")) name = name.Substring(0, name.Length - 5);
            return name;
        }

        public static string DefaultTitle(Type model) 
        {
            return ModelName(model).CamelSpace();
        }
    }
}