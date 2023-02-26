using ASU.Core.Enums;

namespace ASU.Infrastructure.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(
            string message = "resource not found",
            bool isLogging = true,
            ExceptionLevel exceptionLevel = ExceptionLevel.Info)
            : base(exceptionLevel,
                System.Net.HttpStatusCode.NotFound,
                isLogging,
                message)
        {
        }
    }
}
