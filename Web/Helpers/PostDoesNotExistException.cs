namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    public class PostDoesNotExistException: Exception
    {
        //TODO: Fields for structured logging.
        public PostDoesNotExistException(string message): base(message)
        {
            
        }
    }
}