namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}