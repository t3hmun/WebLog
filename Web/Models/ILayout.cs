namespace t3hmun.WLog.Web.Model
{
    using System;
    public interface ILayout
    {
        public string Title { get; protected set; }      
        public Type[] Menu { get; protected set; }      
    }
}