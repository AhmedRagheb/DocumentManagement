using System;
using System.Net;
using System.Threading.Tasks;
using DocumentManagement.DataAccess;
using DocumentManagement.Domain;
using DocumentManagement.Exceptional;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.Api.Middlewares
{
	/// <summary>
	/// Error Handling Middleware
	/// </summary>
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlingMiddleware> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next.</param>
		/// <param name="logger">The logging service.</param>
		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		/// <summary>
		/// Invokes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>Task</returns>
		public async Task Invoke(HttpContext context /* other scoped dependencies */)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var error = new ErrorModel
			{
				Code = ErrorCodes.UnKnownError,
				Messaage = "UnKnownError"
			};

			switch (exception)
			{
				case ServiceException serviceException:
					error.Code = serviceException.ErrorCode;
					error.Messaage = serviceException.Message;
					break;

				case DataAccessException dataAccessException:
					error.Code = dataAccessException.ErrorCode;
					error.Messaage = dataAccessException.Message;
					break;

				default:
					break;
			}

			LogException(exception, context, error.Code);

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			await context.Response.WriteAsync(error.Serialize());

			return;
		}

		private void LogException(Exception exception, HttpContext context, ErrorCodes errorCode)
		{
			_logger.LogError(exception, $"ERROR CODE {(int)errorCode}");

		}
	}
}
