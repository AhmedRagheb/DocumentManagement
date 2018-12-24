using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Settings
{
	/// <summary>
	/// CORS Settings
	/// </summary>
	internal static class CorsSettings
    {
		/// <summary>
		/// The cors policy for master/prod
		/// </summary>
		public const string CorsPolicy = "AllowAll";

		/// <summary>
		/// Adds the cors settings.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>services</returns>
		public static IServiceCollection AddCorsSettings(this IServiceCollection services)
		{
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicy,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
		}

		/// <summary>
		/// Uses the cors settings.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <returns>app</returns>
		public static IApplicationBuilder UseCorsSettings(this IApplicationBuilder app)
		{
			app.UseCors(CorsPolicy);

			return app;
		}
	}
}
