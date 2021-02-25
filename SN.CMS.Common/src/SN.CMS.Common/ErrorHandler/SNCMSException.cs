using System;

namespace SN.CMS.Common.ErrorHandler
{
    public class SNCMSException : Exception
    {
        public string Code { get; }

        public SNCMSException()
        {
        }

        public SNCMSException(string code)
        {
            Code = code;
        }

        public SNCMSException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
