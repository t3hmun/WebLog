namespace t3hmun.WLog.Web.Model
{
    using System;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public abstract class BaseModel : PageModel
    {
        public string Title { get; protected set; }

        public static string DefaultTitle(Type modelType)
        {            
            // TODO
            return "";
        }
    }
}