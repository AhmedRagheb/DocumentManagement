using DocumentManagement.Exceptional;
using System;
using System.Runtime.Serialization;

namespace DocumentManagement.DataAccess
{
    [Serializable]
    public class DataAccessException : Exception
    {
        public ErrorCodes ErrorCode { get; }

        public DataAccessException() : this(ErrorCodes.InternalError)
        {
            // default constructor
        }

        public DataAccessException(string message) : this(ErrorCodes.InternalError, message)
        {
            // default constructor
        }

        public DataAccessException(string message, Exception innerException) : this(ErrorCodes.InternalError, message, innerException)
        {
            // default constructor
        }

        public DataAccessException(ErrorCodes errorCode) : base(errorCode.ToString())
        {
            // Constructor without message
            ErrorCode = errorCode;
        }

        public DataAccessException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public DataAccessException(ErrorCodes errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}