﻿namespace t3hmun.WebLog.Web.Helpers
{
    using System;

    public class ParseException : Exception
    {
        //TODO: Fields for structured logging.
        public ParseException(string message) : base(message)
        {
        }
    }
}