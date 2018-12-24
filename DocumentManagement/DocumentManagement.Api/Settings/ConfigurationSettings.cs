using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Settings
{
	/// <summary>
	/// Authentication Settings
	/// </summary>
	public static class ConfigurationSettings
	{
		/// <summary>
		/// Adds the authentication.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration object containing app config.</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(configuration);

			return services;
		}
	}
}
