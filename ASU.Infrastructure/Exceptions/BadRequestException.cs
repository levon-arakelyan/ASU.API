using ASU.Core.Enums;
using System.Net;

namespace ASU.Infrastructure.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message,
            ExceptionLevel exceptionLevel = ExceptionLevel.Warning,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest,
            bool isLogging = true)
            : base(exceptionLevel, httpStatusCode, isLogging, message)
        {

        }
    }
}
