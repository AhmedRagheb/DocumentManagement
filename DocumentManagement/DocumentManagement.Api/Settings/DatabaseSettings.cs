using DocumentManagement.DataAccess.DBContext;
using DocumentManagement.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Settings
{
	/// <summary>
	/// Database Settings
	/// </summary>
	public static class DatabaseSettings
	{
		/// <summary>
		/// Configures Database services
		/// </summary>
		/// <param name="services">The services reference to configure.</param>
		/// <param name="dbSettingsModel">The database settings model.</param>
		/// <returns>
		/// IServiceCollection
		/// </returns>
		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var dbSettingsModel = configuration.GetSection("ConnectionStrings").Get<DataBaseSettingsModel>();

			services.AddDbContext<DocumentDbContext>(options => options.UseSqlServer(dbSettingsModel.DbConnectionString));
			services.AddScoped<IDocumentDbContext>(provider => provider.GetService<DocumentDbContext>());

			return services;
		}
	}
}
