using System;

namespace AspNetMvc_MultiTenant
{
    public class Office365AssertFailedException : Exception
    {
        public Office365AssertFailedException(string message) : base (message)
        {
        }
    }


    public class Assert
    {
        public static void ThrowExceptionIfNull(object obj, string message)
        {
            if(obj == null)
            {
                throw new Office365AssertFailedException(message);
            }
        }

        public static void ThrowExceptionIfIsNullOrWhiteSpace(string text, string message)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                throw new Office365AssertFailedException(message);
            }
        }
    }
}