using DocumentManagement.DataAccess.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Settings
{
	public static class SeedDatabaseSettings
	{
		public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				// EnsureCreated is used here as this project for testing purpose but in production Migrate must be used.
				var context = serviceScope.ServiceProvider.GetService<DocumentDbContext>();

				context.Database.EnsureCreated();
				context.EnsureSeeded();
			}

			return app;
		}
	}
}
