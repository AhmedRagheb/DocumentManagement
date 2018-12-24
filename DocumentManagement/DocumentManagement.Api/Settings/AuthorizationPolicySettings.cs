using DocumentManagement.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Settings
{
	/// <summary>
	/// Authorization policy settings.
	/// </summary>
	/// <returns>IServiceCollection</returns>
	public static class AuthorizationPolicySettings
	{
		/// <summary>
		/// Adds the authorization policies.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider();
			var authorizationService = serviceProvider.GetService<IAuthorizationService>();
			var policies = authorizationService.GetAllPolicies();

			services.AddAuthorization(options =>
			{
				foreach (var policy in policies)
				{
					options.AddPolicy(policy.Key, x => x.RequireRole(policy.Value));
				}
			});

			return services;
		}
	}
}
