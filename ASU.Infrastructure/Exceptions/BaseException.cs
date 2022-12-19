using ASU.Core.Enums;
using System.Net;

namespace ASU.Infrastructure.Exceptions
{
    public class BaseException : Exception
    {
        public ExceptionLevel ExceptionLevel { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public bool Log { get; }

        public BaseException(
            ExceptionLevel exceptionLevel = ExceptionLevel.Danger,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest,
            bool log = true,
            string message = "") :
            base(message)
        {
            ExceptionLevel = exceptionLevel;
            HttpStatusCode = httpStatusCode;
            Log = log;
        }
    }
}
