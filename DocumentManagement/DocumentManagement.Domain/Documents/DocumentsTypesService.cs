using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.Abstractions.Repositories;
using DocumentManagement.Domain.Helpers;
using DocumentManagement.Models;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Domain
{
	public class DocumentsTypesService : IDocumentsTypesService
	{
		private readonly IDocumentsTypesRepository _documentsTypesRepository;

		public DocumentsTypesService(IDocumentsTypesRepository documentsTypesRepository)
		{
			_documentsTypesRepository = documentsTypesRepository;
		}

		public async Task<IReadOnlyList<DocumentTypeModel>> GetDocumentsTypes()
		{
			var documentsTypes = await _documentsTypesRepository.GetDocumentsTypesAsync().ConfigureAwait(false);
			var documentsTypesModel = documentsTypes.Select(document => document.MapToDocumentTypeModel()).ToList();

			return documentsTypesModel.AsReadOnly();
		}
	}
}
