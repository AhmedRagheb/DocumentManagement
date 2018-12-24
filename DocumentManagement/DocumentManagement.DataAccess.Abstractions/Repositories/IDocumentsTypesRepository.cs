using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.Abstractions.Entities;

namespace DocumentManagement.DataAccess.Abstractions.Repositories
{
	public interface IDocumentsTypesRepository
	{
		Task<List<DocumentType>> GetDocumentsTypesAsync();

		Task<DocumentType> GetDocumentTypeAsync(string contentType);
	}
}