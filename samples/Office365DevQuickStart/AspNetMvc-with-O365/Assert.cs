using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc_with_O365
{
    public class AssertFailedException : Exception
    {
        public AssertFailedException(string message) : base (message)
        {
        }
    }


    public class Assert
    {
        public static void ThrowExceptionIfNull(object obj, string message)
        {
            if(obj == null)
            {
                throw new AssertFailedException(message);
            }
        }

        public static void ThrowExceptionIfIsNullOrWhiteSpace(string text, string message)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                throw new AssertFailedException(message);
            }
        }
    }
}