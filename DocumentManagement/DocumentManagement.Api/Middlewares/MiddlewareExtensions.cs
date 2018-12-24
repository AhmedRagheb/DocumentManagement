using Microsoft.AspNetCore.Builder;

namespace DocumentManagement.Api.Middlewares
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ErrorHandlingMiddleware>();
		}
	}
}
