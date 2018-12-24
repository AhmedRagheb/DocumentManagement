using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DocumentManagement.Api.Settings;
using DocumentManagement.Api.Middlewares;

namespace DocumentManagement.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add authentication 
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = CustomAuthOptions.DefaultScheme;
				options.DefaultChallengeScheme = CustomAuthOptions.DefaultScheme;
			})
			.AddCustomAuth(options =>
			{
				options.AuthKey = "custom auth key";
			});

			//services.AddConfigurations(Configuration);
			services.AddCorsSettings();
			services.AddDatabase(Configuration);
			services.AddDependencyInjectionServices(Configuration);
			services.AddAuthorizationPolicies();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.SeedDatabase();
			app.UseStaticFiles();
			app.UseCorsSettings();
			app.UseAuthentication();
			app.UseErrorHandling();
			loggerFactory.AddFile("Logs/log-{Date}.txt");
			app.UseMvc();
		}
	}
}
