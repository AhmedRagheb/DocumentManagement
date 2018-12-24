using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DocumentManagement.Domain;
using DocumentManagement.DataAccess;
using DocumentManagement.DataAccess.Repositories;
using DocumentManagement.Domain.Abstractions.Authorization;
using DocumentManagement.Domain.Abstractions;
using DocumentManagement.Domain.Common;
using DocumentManagement.DataAccess.Abstractions;
using DocumentManagement.DataAccess.Abstractions.Repositories;

namespace DocumentManagement.Api.Settings
{
	/// <summary>
	/// Dependency Injection Settings
	/// </summary>
	public static class DependencyInjectionSettings
	{
		/// <summary>
		/// Configures services
		/// </summary>
		/// <param name="services">The services reference to configure.</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IDateService, DateService>();
			services.AddTransient<IFileService, FileService>();

			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IDocumentsTypesRepository, DocumentsTypesRepository>();
			services.AddTransient<IDocumentsRepository, DocumentsRepository>();

			services.AddTransient<IAuthorizationService, AuthorizationService>();
			services.AddTransient<IDocumentsDownloadFileService, DocumentsDownloadFileService>();
			services.AddTransient<IDocumentsUplaodFileService, DocumentsUplaodFileService>();
			services.AddTransient<IDocumentsTypesService, DocumentsTypesService>();
			services.AddTransient<IDocumentsService, DocumentsService>();

			return services;
		}
	}
}
