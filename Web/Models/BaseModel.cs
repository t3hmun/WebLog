namespace t3hmun.WLog.Web.Model
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class BaseModel : PageModel, IHasTitle
    {
        public string Title { get; protected set; }
    }
}