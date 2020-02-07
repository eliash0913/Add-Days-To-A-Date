using System;

namespace Add_Days_To_A_Date
{
    /// <summary>
    /// DateFormateException to handle format error.
    /// </summary>
    class DateFormatException : Exception
    {
        public DateFormatException()
        { 
        }
        public DateFormatException(string message) : base(message)
        {
        }
    }
}
