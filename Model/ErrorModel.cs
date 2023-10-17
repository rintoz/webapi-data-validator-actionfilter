using System;

namespace DataValidator.Model
{
    [Serializable]
    public class ErrorModel
    {
        private readonly Exception exception;

        public string Error { get; set; }

        public string StackTrace { get { return exception?.StackTrace; } }

        public string InnterException { get { return exception?.InnerException?.ToString(); } }

        public ErrorModel()
        {
        }

        public ErrorModel(Exception exception)
        {
            this.exception = exception;
            Error = exception.Message;
        }
    }
}