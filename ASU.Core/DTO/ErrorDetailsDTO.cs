using ASU.Core.Enums;
using System.Net;

namespace MedMinder.Caregivers.Core.Exceptions
{
    public class ErrorDetailsDTO
    {
        public string Message { get; set; }
        public ExceptionLevel ExceptionLevel { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
