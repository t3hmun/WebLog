namespace t3hmun.WLog.Web.Model
{
    using t3hmun.WLog.Web.Helpers;
    using System;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public abstract class BaseModel : PageModel
    {
        public string Title { get; protected set; }

        public BaseModel()
        {
            Title = DefaultTitle(GetType());
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