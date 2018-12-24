using DocumentManagement.Exceptional;
using System;
using System.Runtime.Serialization;

namespace DocumentManagement.Domain
{
	[Serializable]
	public class ServiceException : Exception
	{
		public ErrorCodes ErrorCode { get; set; }
		public ServiceException() : this(ErrorCodes.InternalError)
		{
			// default constructor
		}

		public ServiceException(string message) : this(ErrorCodes.InternalError, message)
		{
			// default constructor
		}

        public ServiceException(string message, Exception innerException) : this(ErrorCodes.InternalError, message, innerException)
        {
            // default constructor
        }

        public ServiceException(ErrorCodes errorCode) : base(errorCode.ToString())
		{
			// Constructor without message
			ErrorCode = errorCode;
		}

		public ServiceException(ErrorCodes errorCode, string message) : base(message)
		{
			ErrorCode = errorCode;
		}

        public ServiceException(ErrorCodes errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
