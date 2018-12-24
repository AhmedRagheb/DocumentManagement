using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.Abstractions.Entities;

namespace DocumentManagement.DataAccess.Abstractions.Repositories
{
	public interface IDocumentsRepository
	{
		Task AddDocumentAsync(Document document);

		Task DeleteDocumentAsync(int documentId);

		Task<List<Document>> GetDocumentsAsync();

		Task<Document> GetDocumentAsync(int documentId);

		void UpdateDocumentAsync(Document document);
	}
}